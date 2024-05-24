using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Game {

    public class LabyrinthSpawner : MonoBehaviour {

        [SerializeField]
        private LabyrinthCell[] _cellsPrefabs;

        [SerializeField]
        private LabyrinthCell[] _deadEndCellsPrefabs;
        private Dictionary<LabyrinthCell.Direction, LabyrinthCell> _cachedDeadEndCellsPrefabs;

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

        public void Spawn() {
            _cachedDeadEndCellsPrefabs = _deadEndCellsPrefabs.ToDictionary(prefab => prefab.AvailablePositions.FirstOrDefault().Key);
            _field = new LabyrinthCell[_maxCellsCount, _maxCellsCount];
            var startCellFieldPosition = new Vector2Int(_maxCellsCount / 2, 0);
            _field[startCellFieldPosition.x, startCellFieldPosition.y] = _startCell;
            _spawnedSells.Add(_startCell);
            _startCell.fieldPosition = startCellFieldPosition;
            AddCells(_startCell);
            AddDeadEndCells();
        }

        private void AddDeadEndCells() {
            foreach(var cell in _spawnedSells) {
                foreach(var positions in cell.AvailablePositions) {
                    if(GetCellByFieldPosition(cell.fieldPosition + positions.Value.GetDirectionVector()) != null) {
                        continue;
                    }
                    var deadEndPrefab = _cachedDeadEndCellsPrefabs[LabyrinthDirectionUtils.ConvertVectorToDirection(positions.Value.GetDirectionVector() * -1)];
                    InstantiateCell(cell, positions.Value.GetDirectionVector(),deadEndPrefab);
                }
            }
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
                var requiredDirection = LabyrinthDirectionUtils.ConvertVectorToDirection(positionDirection * -1);
                var availableCells = _cellsPrefabs.Where(cell => cell.AvailablePositions.ContainsKey(requiredDirection)).ToArray();
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

        private bool FieldPositionIsValid(Vector2Int cellFieldPosition) {
            return _maxCellsCount > cellFieldPosition.x && _maxCellsCount > cellFieldPosition.y && cellFieldPosition.x >= 0 && cellFieldPosition.y >= 0;
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
    }
}
