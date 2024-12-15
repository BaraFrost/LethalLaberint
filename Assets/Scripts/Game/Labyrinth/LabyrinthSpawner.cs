using Data;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Game {

    public class LabyrinthSpawner : MonoBehaviour {

        [Serializable]
        private struct FieldOffsets {
            public int startCellYPosition;
            public int additionalFieldSize;
            public int deadEndOffset;
        }

        [SerializeField]
        private LabyrinthCell _fonCellPrefab;

        [SerializeField]
        private LabyrinthCellsContainer _labyrinthCellsContainer;

        [SerializeField]
        private GameObject _floor;


        [SerializeField]
        private float _cellSize;

        [SerializeField]
        private FieldOffsets _fieldOffsets;

        [SerializeField]
        private StartCellsContainer _startCellsContainer;

        [SerializeField]
        private NavMeshSurface _navMeshSurface;

        private LabyrinthCell _startCell;
        private StartLabyrinthCells _spawnedStartCells;
        private LabyrinthCell[] _additiveStartCells;

        private int LabyrinthSize => _difficultyProgressionConfig.LabyrinthSize;
        private int MaxCellsCount => _difficultyProgressionConfig.LabyrinthCellsCount;
        private int MaxEpoch => _difficultyProgressionConfig.LabyrinthEpoch;
        private int FullLabyrinthSize => LabyrinthSize + _fieldOffsets.additionalFieldSize;

        private LabyrinthCell[,] _field;
        private List<LabyrinthCell> _spawnedCells = new List<LabyrinthCell>();
        public List<LabyrinthCell> SpawnedCells => _spawnedCells;
        private DifficultyProgressionConfig _difficultyProgressionConfig;
        private GameEntitiesContainer _gameEntitiesContainer;

#if UNITY_EDITOR
        [Button]
        public void Respawn() {
            if (!EditorApplication.isPlaying) {
                return;
            }
            _spawnedCells.Remove(_startCell);
            _startCell.Clear();
            foreach (var startCell in _additiveStartCells) {
                startCell.Clear();
                _spawnedCells.Remove(startCell);
            }
            foreach (var cell in _spawnedCells) {
                Destroy(cell.gameObject);
            }
            _spawnedCells.Clear();
            Spawn(_gameEntitiesContainer);
        }
#endif

        public void Spawn(GameEntitiesContainer gameEntitiesContainer) {
            _gameEntitiesContainer = gameEntitiesContainer;
            _difficultyProgressionConfig = Account.Instance.DifficultyProgressionConfig;
            SpawnStartCells();
            SetStartData();
            if (_spawnedStartCells.NeedGenerateCells) {
                AddCells();
            }
            AddDeadEndCells();
            AddFonCells();
            AddLabyrinthToStaticBatching();
            _navMeshSurface.BuildNavMesh();
            gameEntitiesContainer.cellsContainer = new SpawnedLabyrinthCellsContainer(_spawnedCells, FullLabyrinthSize, _field, _spawnedStartCells);
        }

        private void SpawnStartCells() {
            var startLabyrinthCells = _startCellsContainer.GetRandomStartCells(Account.Instance.CurrentStage);
            _spawnedStartCells = Instantiate(startLabyrinthCells, transform);
            _startCell = _spawnedStartCells.StartCell;
        }

        private void AddLabyrinthToStaticBatching() {
            var objectsToCombine = _spawnedCells.SelectMany(cell => cell.Walls).ToArray();
            StaticBatchingUtility.Combine(objectsToCombine, _floor.gameObject);
        }

        private void SetStartData() {
            _field = new LabyrinthCell[FullLabyrinthSize, FullLabyrinthSize];
            var startCellFieldPosition = new Vector2Int(FullLabyrinthSize / 2, _fieldOffsets.startCellYPosition);
            _field[startCellFieldPosition.x, startCellFieldPosition.y] = _startCell;
            _spawnedCells.Add(_startCell);
            _startCell.fieldPosition = startCellFieldPosition;
            SetFloorPosition();
            foreach (var additiveStartCell in _spawnedStartCells.StartCells) {
                var positionDifference = additiveStartCell.transform.position - _startCell.transform.position;
                var fieldPosition = Vector2Int.RoundToInt(new Vector2(positionDifference.x, positionDifference.z) / _cellSize) + _startCell.fieldPosition;
                _field[fieldPosition.x, fieldPosition.y] = additiveStartCell;
                _spawnedCells.Add(additiveStartCell);
                additiveStartCell.fieldPosition = fieldPosition;
                ConnectNearestCells(additiveStartCell);
            }
        }

        private void SetFloorPosition() {
            var floorFieldPosition = new Vector2Int(FullLabyrinthSize / 2, FullLabyrinthSize / 2) - _startCell.fieldPosition;
            var floorPosition = FieldPostionToVector3(floorFieldPosition) * _cellSize + _startCell.gameObject.transform.position;
            floorPosition.y = _floor.transform.position.y;
            _floor.transform.position = floorPosition;
            _floor.transform.localScale = (Vector3.one * (FullLabyrinthSize - _fieldOffsets.deadEndOffset) * _cellSize / 10) * _floor.transform.localScale.x / _floor.transform.lossyScale.x;
        }

        private Vector3 FieldPostionToVector3(Vector2Int vector) {
            return new Vector3(vector.x, 0, vector.y);
        }

        private void AddFonCells() {
            var zeroPosition = _startCell.gameObject.transform.position - new Vector3(_startCell.fieldPosition.x, 0, _startCell.fieldPosition.y) * _cellSize;
            for (int i = 0; i < FullLabyrinthSize; i++) {
                for (int j = 0; j < FullLabyrinthSize; j++) {
                    if (_field[i, j] != null) {
                        continue;
                    }
                    var newCellPosition = zeroPosition + (new Vector3(i, 0, j) * _cellSize);
                    _field[i, j] = Instantiate(_fonCellPrefab, newCellPosition, Quaternion.identity, transform);
                    _spawnedCells.Add(_field[i, j]);
                }
            }
        }

        private void AddDeadEndCells() {
            var deadEnds = new List<LabyrinthCell>();
            foreach (var cell in _spawnedCells) {
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
            _spawnedCells.AddRange(deadEnds);
        }


        private void AddCells() {
            LabyrinthCell[] availableCells;
            var epoch = _spawnedStartCells.StartEpoch;
            do {
                availableCells = _spawnedCells.Where(cell => cell.RealtimeAvailablePositions.Count > 0).ToArray();
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
                        _spawnedCells.Add(newCellInstance);
                        _field[newFieldCellPosition.x, newFieldCellPosition.y] = newCellInstance;
                        ConnectNearestCells(newCellInstance);
                    }
                }
                epoch++;
            }
            while (epoch < MaxEpoch && availableCells != null && availableCells.Length > 0 && _spawnedCells.Count < MaxCellsCount);
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
            return LabyrinthSize > cellFieldPosition.x
                && LabyrinthSize > cellFieldPosition.y
                && cellFieldPosition.x >= _fieldOffsets.additionalFieldSize
                && cellFieldPosition.y >= _fieldOffsets.additionalFieldSize;
        }

        private bool FieldPositionIsValidForDeadEnd(Vector2Int cellFieldPosition) {
            return FullLabyrinthSize - _fieldOffsets.deadEndOffset > cellFieldPosition.x
                && FullLabyrinthSize - _fieldOffsets.deadEndOffset > cellFieldPosition.y
                && cellFieldPosition.x >= _fieldOffsets.deadEndOffset
                && cellFieldPosition.y >= _fieldOffsets.deadEndOffset;
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
