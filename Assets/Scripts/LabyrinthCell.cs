using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Game {

    public class LabyrinthCell : MonoBehaviour {

        public enum Direction {
            Left,
            Right,
            Up,
            Down,
        }

        [Serializable]
        public class AvailablePosition {

            [SerializeField]
            private Direction _direction;
            public Direction Direction => _direction;

            public Vector2Int GetDirectionVector() {
                return LabyrinthDirectionUtils.ConvertDirectionToVector(Direction);
            }
        }

        [SerializeField]
        private List<AvailablePosition> _availablePositions;

        private Dictionary<Direction, AvailablePosition> _realtimeAvailablePositions;
        public Dictionary<Direction, AvailablePosition> AvailablePositions {
            get {
                if (_realtimeAvailablePositions == null) {
                    _realtimeAvailablePositions = _availablePositions.ToDictionary(position => position.Direction);
                }
                return _realtimeAvailablePositions;
            }
            private set {
                _realtimeAvailablePositions = value;
            }
        }

        public bool HasAvailablePositions => AvailablePositions.Count > 0;

        private Dictionary<Direction, LabyrinthCell> _nearestCells = new Dictionary<Direction, LabyrinthCell>();
        public Dictionary<Direction, LabyrinthCell> NearestCells => _nearestCells;

        public Vector2Int fieldPosition;

        public bool TryToAddNearestCell(Direction direction, LabyrinthCell labyrinthCell) {
            if (AvailablePositions.ContainsKey(direction)) {
                return false;
            }
            _nearestCells.Add(direction, labyrinthCell);
            AvailablePositions.Remove(direction);
            return true;
        }

        public bool AvailableAllDirections(List<Direction> directions) {
            foreach(var direction in directions) {
                if(!AvailablePositions.ContainsKey(direction)) {
                    return false;
                }
            }
            return true;
        }

        public bool AvailableOnlyThisDirections(List<Direction> directions) {
            return directions.Count == AvailablePositions.Count && AvailableAllDirections(directions);
        }
    }
}

