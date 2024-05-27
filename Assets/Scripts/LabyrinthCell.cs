using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Game {

    public class LabyrinthCell : MonoBehaviour {

        public enum Direction {
            Left = 0,
            Up = 1,
            Right = 2,
            Down = 3,
        }

        public enum ConnectionType {
            
        }

        [Serializable]
        public class AvailablePosition {

            [SerializeField]
            private Direction _direction;
            public Direction Direction => _direction;

            [SerializeField]
            private ConnectionType _connectionType;
            public ConnectionType ConnectionType => _connectionType;

            public AvailablePosition(Direction direction, ConnectionType connectionType) {
                _direction = direction;
                _connectionType = connectionType;
            }

            public Vector2Int GetDirectionVector() {
                return LabyrinthDirectionUtils.ConvertDirectionToVector(Direction);
            }

            public AvailablePosition GetRotatedClone(int rotation) {
                return new AvailablePosition(LabyrinthDirectionUtils.RotateDirection(_direction, rotation), _connectionType);
            }
        }

        [SerializeField]
        private List<AvailablePosition> _availablePositions;
        public List<AvailablePosition> AvailablePositions => _availablePositions;

        private Dictionary<Direction, AvailablePosition> _realtimeAvailablePositions;
        public Dictionary<Direction, AvailablePosition> RealtimeAvailablePositions {
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

        public bool HasAvailablePositions => RealtimeAvailablePositions.Count > 0;

        private Dictionary<Direction, LabyrinthCell> _nearestCells = new Dictionary<Direction, LabyrinthCell>();
        public Dictionary<Direction, LabyrinthCell> NearestCells => _nearestCells;

        [NonSerialized]
        public Vector2Int fieldPosition;

        public void Clear() {
            _realtimeAvailablePositions = null;
            _nearestCells.Clear();
            fieldPosition = Vector2Int.zero;
        }

        public bool TryToAddNearestCell(Direction direction, LabyrinthCell labyrinthCell) {
            if (RealtimeAvailablePositions.ContainsKey(direction)) {
                return false;
            }
            _nearestCells.Add(direction, labyrinthCell);
            RealtimeAvailablePositions.Remove(direction);
            return true;
        }

        public bool AvailableAllDirections(List<Direction> directions) {
            foreach(var direction in directions) {
                if(!RealtimeAvailablePositions.ContainsKey(direction)) {
                    return false;
                }
            }
            return true;
        }

        public bool AvailableOnlyThisDirections(List<Direction> directions) {
            return directions.Count == RealtimeAvailablePositions.Count && AvailableAllDirections(directions);
        }

        public void SetRealtimeAvailablePositions(Dictionary<Direction, AvailablePosition> realtimeAvailablePositions) {
            _realtimeAvailablePositions = realtimeAvailablePositions;
        }
    }
}