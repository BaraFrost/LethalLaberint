using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Game {

#if UNITY_EDITOR
    [CustomEditor(typeof(LabyrinthSpawner))]
    public class LabyrinthSpawnerEditor: Editor {
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
        private LabyrinthCell[] _cellsPrefabs;

        [SerializeField]
        private int labyrinthSize;

        [SerializeField]
        private float _cellSize;

        [SerializeField]
        private int _maxCellsCount;

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
            _field = new LabyrinthCell[labyrinthSize + 2, labyrinthSize + 2];
            var startCellFieldPosition = new Vector2Int(labyrinthSize / 2, 1);
            _field[startCellFieldPosition.x, startCellFieldPosition.y] = _startCell;
            _spawnedSells.Add(_startCell);
            _startCell.fieldPosition = startCellFieldPosition;
            AddCells(_startCell);
            AddDeadEndCells();
        }

        private void AddDeadEndCells() {
            var deadEnds = new List<LabyrinthCell>();
            foreach (var cell in _spawnedSells) {
                foreach (var positions in cell.AvailablePositions) {
                    var fieldPosition = cell.fieldPosition + positions.Value.GetDirectionVector();
                    if (GetCellByFieldPositionForDeadEnd(fieldPosition) != null) {
                        continue;
                    }
                    var requiredDirections = GetAllRequiredDirections(fieldPosition);
                    var deadEndPrefabs = _cellsPrefabs.Where(cell => cell.AvailableOnlyThisDirections(requiredDirections)).ToArray();
                    if (deadEndPrefabs == null || deadEndPrefabs.Length == 0) {
                        var sb = new StringBuilder();
                        foreach (var direction in requiredDirections) {
                            sb.Append($"{direction},");
                        }
                        Debug.Log(sb);
                        continue;
                    }
                    var deadEndInstance = InstantiateCell(cell, positions.Value.GetDirectionVector(), deadEndPrefabs[UnityEngine.Random.Range(0, deadEndPrefabs.Count() - 1)]);
                    deadEndInstance.fieldPosition = fieldPosition;
                    _field[fieldPosition.x, fieldPosition.y] = deadEndInstance;
                    deadEnds.Add(deadEndInstance);
                }
            }
            _spawnedSells.AddRange(deadEnds);
        }

        private void AddCells(LabyrinthCell labyrinthCell) {
            var availablePositions = labyrinthCell.AvailablePositions;
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
                var availableCells = _cellsPrefabs.Where(cell => cell.AvailableAllDirections(requiredDirections)).ToArray();
                if (availableCells.Count() == 0) {
                    continue;
                }
                var randomCellToSpawn = availableCells[UnityEngine.Random.Range(0, availableCells.Count() - 1)];
                var newCellInstance = InstantiateCell(labyrinthCell, positionDirection, randomCellToSpawn);
                newCellInstance.fieldPosition = newFieldCellPosition;
                _spawnedSells.Add(newCellInstance);
                _field[newFieldCellPosition.x, newFieldCellPosition.y] = newCellInstance;
                ConnectNearestCells(newCellInstance);
                AddCells(newCellInstance);
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

        private LabyrinthCell InstantiateCell(LabyrinthCell labyrinthCell, Vector2Int direction, LabyrinthCell cellToSpawn) {
            var newCellPosition = labyrinthCell.transform.position + (new Vector3(direction.x, 0, direction.y) * _cellSize);
            return Instantiate(cellToSpawn, newCellPosition, Quaternion.identity, transform);
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
