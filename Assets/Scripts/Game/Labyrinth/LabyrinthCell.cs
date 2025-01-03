using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace Game {

    public class LabyrinthCell : MonoBehaviour, ILabyrinthEntity {

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

        [SerializeField]
        private bool _closedCell;
        public bool ClosedCell => _closedCell;

        [SerializeField]
        private GameObject[] _walls;
        public GameObject[] Walls => _walls;

        public bool cellBusy;

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
            foreach (var direction in directions) {
                if (!RealtimeAvailablePositions.ContainsKey(direction)) {
                    return false;
                }
            }
            return true;
        }

        public bool AvailableOnlyThisDirections(List<Direction> directions) {
            return directions.Count == RealtimeAvailablePositions.Count && AvailableAllDirections(directions);
        }

        public bool AvailableOnlyThisDirectionsDefault(List<Direction> directions) {
            if (directions.Count != _availablePositions.Count) {
                return false;
            }
            foreach (var direction in directions) {
                if (_availablePositions.FirstOrDefault(position => position.Direction == direction) == null) {
                    return false;
                }
            }
            return true;
        }

        public void SetRealtimeAvailablePositions(Dictionary<Direction, AvailablePosition> realtimeAvailablePositions) {
            _realtimeAvailablePositions = realtimeAvailablePositions;
        }

#if UNITY_EDITOR
        [Button]
        private void RotateLeft() {
            CalculateAvailablePositions(-90);
        }

        [Button]
        private void RotateRight() {
            CalculateAvailablePositions(90);
        }

        private void CalculateAvailablePositions(int rotation) {
            gameObject.transform.rotation *= Quaternion.Euler(0, rotation, 0);
            var availablePositions = new List<AvailablePosition>();
            foreach (var position in _availablePositions) {
                var rotatedAvailablePosition = position.GetRotatedClone(rotation);
                availablePositions.Add(rotatedAvailablePosition);
            }
            _availablePositions = availablePositions;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}