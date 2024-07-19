using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game {

    public class RotatedLabyrinthCell {

        private LabyrinthCell _labyrinthCell;
        public LabyrinthCell LabyrinthCell => _labyrinthCell;

        private int _rotation;

        private Dictionary<LabyrinthCell.Direction, LabyrinthCell.AvailablePosition> _availablePositions;
        public Dictionary<LabyrinthCell.Direction, LabyrinthCell.AvailablePosition> AvailablePositions => _availablePositions;

        public RotatedLabyrinthCell(LabyrinthCell labyrinthCell, int rotation) {
            _labyrinthCell = labyrinthCell;
            _rotation = rotation;
            CalculateAvailablePositions();
        }

        private void CalculateAvailablePositions() {
            if (_rotation == 0) {
                _availablePositions = _labyrinthCell.AvailablePositions.ToDictionary(position => position.Direction);
                return;
            }
            _availablePositions = new Dictionary<LabyrinthCell.Direction, LabyrinthCell.AvailablePosition>();
            foreach(var position in _labyrinthCell.AvailablePositions) {
                var rotatedAvailablePosition = position.GetRotatedClone(_rotation);
                _availablePositions.Add(rotatedAvailablePosition.Direction, rotatedAvailablePosition);
            }
        }

        public LabyrinthCell InstantiateCell(LabyrinthCell parentCell, Vector2Int direction, Transform parent, float cellSize) {
            var newCellPosition = parentCell.transform.position + (new Vector3(direction.x, 0, direction.y) * cellSize);
            var cellInstance = Object.Instantiate(_labyrinthCell, newCellPosition, Quaternion.AngleAxis(_rotation, Vector3.up), parent);
            cellInstance.SetRealtimeAvailablePositions(_availablePositions);
            return cellInstance;
        }

        public bool AvailableAllDirections(List<LabyrinthCell.Direction> directions) {
            foreach (var direction in directions) {
                if (!_availablePositions.ContainsKey(direction)) {
                    return false;
                }
            }
            return true;
        }

        public bool AvailableOnlyThisDirections(List<LabyrinthCell.Direction> directions) {
            return directions.Count == _availablePositions.Count && AvailableAllDirections(directions);
        }
    }
}
