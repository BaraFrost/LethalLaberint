using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Game {

#if UNITY_EDITOR
    [CustomEditor(typeof(LabyrinthSpawner))]
    public class LabyrinthSpawnerEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var LabyrinthSpawner = target as LabyrinthSpawner;
            if (GUILayout.Button("Respawn")) {
                LabyrinthSpawner.Respawn();
            }
        }
    }
#endif

    public class LabyrinthSpawner : MonoBehaviour {

        [SerializeField]
        private LabyrinthCell _fonCellPrefab;

        [SerializeField]
        private LabyrinthCellsContainer _labyrinthCellsContainer;

        [SerializeField]
        private int labyrinthSize;

        [SerializeField]
        private float _cellSize;

        [SerializeField]
        private int _maxCellsCount;

        [SerializeField]
        private int _maxEpoch;

        [SerializeField]
        private LabyrinthCell _startCell;

        private LabyrinthCell[,] _field;
        private List<LabyrinthCell> _spawnedSells = new List<LabyrinthCell>();

        public void Respawn() {
            _spawnedSells.Remove(_startCell);
            foreach (var cell in _spawnedSells) {
                Destroy(cell.gameObject);
            }
            _spawnedSells.Clear();
            Spawn();
        }

        public void Spawn() {
            SetStartData();
            AddCellsTest();
            //AddCells(_startCell, epoch: 0);
            AddDeadEndCells();
            AddFonCells();
        }

        private void SetStartData() {
            _field = new LabyrinthCell[labyrinthSize + 2, labyrinthSize + 2];
            var startCellFieldPosition = new Vector2Int(labyrinthSize / 2, 1);
            _field[startCellFieldPosition.x, startCellFieldPosition.y] = _startCell;
            _spawnedSells.Add(_startCell);
            _startCell.fieldPosition = startCellFieldPosition;
        }

        private void AddFonCells() {
            var zeroPosition = _startCell.gameObject.transform.position - new Vector3(labyrinthSize / 2, 0, 1) * _cellSize;
            for (int i = 0; i < labyrinthSize + 2; i++) {
                for (int j = 0; j < labyrinthSize + 2; j++) {
                    if (_field[i, j] != null) {
                        continue;
                    }
                    var newCellPosition = zeroPosition + (new Vector3(i, 0, j) * _cellSize);
                    _field[i, j] = Instantiate(_fonCellPrefab, newCellPosition, Quaternion.identity, transform);
                    _spawnedSells.Add(_field[i, j]);
                }
            }
        }

        private void AddDeadEndCells() {
            var deadEnds = new List<LabyrinthCell>();
            foreach (var cell in _spawnedSells) {
                foreach (var positions in cell.RealtimeAvailablePositions) {
                    var fieldPosition = cell.fieldPosition + positions.Value.GetDirectionVector();
                    if (GetCellByFieldPositionForDeadEnd(fieldPosition) != null) {
                        continue;
                    }
                    var requiredDirections = GetAllRequiredDirections(fieldPosition);
                    var deadEndPrefab = _labyrinthCellsContainer.GetRandomDeadEndCell(requiredDirections);
                    var deadEndInstance = deadEndPrefab.InstantiateCell(cell, positions.Value.GetDirectionVector(), transform, _cellSize);
                    deadEndInstance.fieldPosition = fieldPosition;
                    _field[fieldPosition.x, fieldPosition.y] = deadEndInstance;
                    deadEnds.Add(deadEndInstance);
                }
            }
            _spawnedSells.AddRange(deadEnds);
        }


        private void AddCellsTest() {
            LabyrinthCell[] availableCells;
            var epoch = 0;
            do {
                availableCells = _spawnedSells.Where(cell => cell.RealtimeAvailablePositions.Count > 0).ToArray();
                foreach (var cell in availableCells) {
                    foreach (var position in cell.RealtimeAvailablePositions) {
                        var positionDirection = position.Value.GetDirectionVector();
                        var newFieldCellPosition = cell.fieldPosition + positionDirection;
                        if (!FieldPositionIsValid(newFieldCellPosition) || GetCellByFieldPosition(newFieldCellPosition) != null) {
                            continue;
                        }
                        var requiredDirections = GetAllRequiredDirections(newFieldCellPosition);
                        var randomCellToSpawn = _labyrinthCellsContainer.GetRandomCell(requiredDirections, epoch);
                        var newCellInstance = randomCellToSpawn.InstantiateCell(cell, positionDirection, transform, _cellSize);
                        newCellInstance.fieldPosition = newFieldCellPosition;
                        _spawnedSells.Add(newCellInstance);
                        _field[newFieldCellPosition.x, newFieldCellPosition.y] = newCellInstance;
                        ConnectNearestCells(newCellInstance);
                    }
                }
                epoch++;
            }
            while (epoch < _maxEpoch && availableCells != null && availableCells.Length > 0 && _spawnedSells.Count < _maxCellsCount) ;
        }

        private void AddCells(LabyrinthCell labyrinthCell, int epoch) {
            var availablePositions = labyrinthCell.RealtimeAvailablePositions;
            epoch++;
            if (epoch > _maxEpoch) {
                return;
            }
            foreach (var position in availablePositions) {
                if (_spawnedSells.Count >= _maxCellsCount) {
                    return;
                }
                var positionDirection = position.Value.GetDirectionVector();
                var newFieldCellPosition = labyrinthCell.fieldPosition + positionDirection;
                if (!FieldPositionIsValid(newFieldCellPosition) || GetCellByFieldPosition(newFieldCellPosition) != null) {
                    continue;
                }
                var requiredDirections = GetAllRequiredDirections(newFieldCellPosition);
                var randomCellToSpawn = _labyrinthCellsContainer.GetRandomCell(requiredDirections, epoch);
                var newCellInstance = randomCellToSpawn.InstantiateCell(labyrinthCell, positionDirection, transform, _cellSize);
                newCellInstance.fieldPosition = newFieldCellPosition;
                _spawnedSells.Add(newCellInstance);
                _field[newFieldCellPosition.x, newFieldCellPosition.y] = newCellInstance;
                ConnectNearestCells(newCellInstance);
                AddCells(newCellInstance, epoch);
            }
        }

        private List<LabyrinthCell.Direction> GetAllRequiredDirections(Vector2Int fieldCellPosition) {
            var result = new List<LabyrinthCell.Direction>();
            foreach (var direction in LabyrinthDirectionUtils.directionToVector) {
                var nearestCellPosition = fieldCellPosition + direction.Value;
                var nearestCell = GetCellByFieldPositionForDeadEnd(nearestCellPosition);
                if (nearestCell != null) {
                    result.Add(direction.Key);
                }
            }
            return result;
        }

        private bool FieldPositionIsValid(Vector2Int cellFieldPosition) {
            return labyrinthSize > cellFieldPosition.x && labyrinthSize > cellFieldPosition.y && cellFieldPosition.x >= 1 && cellFieldPosition.y >= 1;
        }

        private bool FieldPositionIsValidForDeadEnd(Vector2Int cellFieldPosition) {
            return labyrinthSize + 2 > cellFieldPosition.x && labyrinthSize + 2 > cellFieldPosition.y && cellFieldPosition.x >= 0 && cellFieldPosition.y >= 0;
        }

        private void ConnectNearestCells(LabyrinthCell labyrinthCell) {
            foreach (var direction in LabyrinthDirectionUtils.directionToVector) {
                var nearestCellPosition = labyrinthCell.fieldPosition + direction.Value;
                var nearestCell = GetCellByFieldPosition(nearestCellPosition);
                if (nearestCell == null) {
                    continue;
                }
                nearestCell.TryToAddNearestCell(LabyrinthDirectionUtils.ConvertVectorToDirection(direction.Value * -1), labyrinthCell);
                labyrinthCell.TryToAddNearestCell(direction.Key, nearestCell);
            }
        }

        private LabyrinthCell GetCellByFieldPosition(Vector2Int cellFieldPosition) {
            if (!FieldPositionIsValid(cellFieldPosition)) {
                return null;
            }
            return _field[cellFieldPosition.x, cellFieldPosition.y];
        }

        private LabyrinthCell GetCellByFieldPositionForDeadEnd(Vector2Int cellFieldPosition) {
            if (!FieldPositionIsValidForDeadEnd(cellFieldPosition)) {
                return null;
            }
            return _field[cellFieldPosition.x, cellFieldPosition.y];
        }
    }
}
