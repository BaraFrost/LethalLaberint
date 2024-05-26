using Game;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {

    public static class LabyrinthDirectionUtils {

        public static readonly Dictionary<LabyrinthCell.Direction, Vector2Int> directionToVector = new Dictionary<LabyrinthCell.Direction, Vector2Int>() {
            { LabyrinthCell.Direction.Left, Vector2Int.left },
            { LabyrinthCell.Direction.Right, Vector2Int.right },
            { LabyrinthCell.Direction.Up, Vector2Int.up },
            { LabyrinthCell.Direction.Down, Vector2Int.down },
        };

        public static readonly Dictionary<Vector2Int, LabyrinthCell.Direction> vectorToDirection = new Dictionary<Vector2Int, LabyrinthCell.Direction>() {
            { Vector2Int.left, LabyrinthCell.Direction.Left },
            { Vector2Int.right, LabyrinthCell.Direction.Right },
            { Vector2Int.up, LabyrinthCell.Direction.Up },
            { Vector2Int.down, LabyrinthCell.Direction.Down },
        };

        public static Vector2Int ConvertDirectionToVector(LabyrinthCell.Direction direction) {
            return directionToVector[direction];
        }

        public static LabyrinthCell.Direction ConvertVectorToDirection(Vector2Int vector) {
            return vectorToDirection[vector];
        }

        public static LabyrinthCell.Direction GetOppositeDirection(LabyrinthCell.Direction direction) {
            return vectorToDirection[directionToVector[direction] * -1];
        }

        public static LabyrinthCell.Direction RotateDirection(LabyrinthCell.Direction direction, int rotation) {
            return (LabyrinthCell.Direction)(((rotation / 90) + (int)direction) % Enum.GetValues(typeof(LabyrinthCell.Direction)).Length);
        }
    }
}