using Game;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(LabyrinthCellsContainer), menuName = "Data/LabyrinthCellsContainer")]
    public class LabyrinthCellsContainer : ScriptableObject {

        [System.Serializable]
        private class CellWithWeight {

            [SerializeField]
            private LabyrinthCell _labyrinthCell;
            public LabyrinthCell LabyrinthCell => _labyrinthCell;

            [SerializeField]
            private int _weight;
            public int Weight => _weight;
        }

        private class RotatedCellWithWeight {

            private RotatedLabyrinthCell _labyrinthCell;
            public RotatedLabyrinthCell LabyrinthCell => _labyrinthCell;

            private int _weight;
            public int Weight => _weight;

            public RotatedCellWithWeight(RotatedLabyrinthCell labyrinthCell, int weight) {
                _labyrinthCell = labyrinthCell;
                _weight = weight;
            }
        }

        [System.Serializable]
        private class CellsWithEpoch : CellsContainer {

            [SerializeField]
            private int _maxEpoch;
            public int MaxEpoch => _maxEpoch;
        }

        [System.Serializable]
        private class CellsContainer {

            [SerializeField]
            private CellWithWeight[] _labyrinthCells;
            public CellWithWeight[] LabyrinthCells => _labyrinthCells;

            private List<RotatedCellWithWeight> _rotatedCellsWithWeight;
            public List<RotatedCellWithWeight> RotatedCellsWithWeight {
                get {
                    if (_rotatedCellsWithWeight == null) {
                        GenerateRotatedCells();
                    }
                    return _rotatedCellsWithWeight;
                }
                private set {
                    _rotatedCellsWithWeight = value;
                }
            }

            private void GenerateRotatedCells() {
                RotatedCellsWithWeight = new List<RotatedCellWithWeight>();
                foreach (var prefabWithWeight in _labyrinthCells) {
                    for (int i = 0; i < 4; i++) {
                        var rotatedLabyrinthCell = new RotatedLabyrinthCell(prefabWithWeight.LabyrinthCell, 90 * i);
                        RotatedCellsWithWeight.Add(new RotatedCellWithWeight(rotatedLabyrinthCell, prefabWithWeight.Weight));
                    }
                }
            }

            public RotatedCellWithWeight[] GetAvailableOnlyThisDirectionsCells(List<LabyrinthCell.Direction> directions) {
                return RotatedCellsWithWeight.Where(cell => cell.LabyrinthCell.AvailableOnlyThisDirections(directions)).ToArray();
            }

            public RotatedCellWithWeight[] GetAvailableAllDirectionsCells(List<LabyrinthCell.Direction> directions) {
                return RotatedCellsWithWeight.Where(cell => cell.LabyrinthCell.AvailableAllDirections(directions)).ToArray();
            }
        }

        [SerializeField]
        private CellsWithEpoch[] _cellsEpochs;

        [SerializeField]
        private CellsContainer _allCells;

        public RotatedLabyrinthCell GetRandomCell(List<LabyrinthCell.Direction> requiredDirections, int epochNum) {
            var epoch = GetEpoch(epochNum);
            var availableCells = epoch.GetAvailableAllDirectionsCells(requiredDirections);
            if (availableCells.Length == 0) {
                availableCells = _allCells.GetAvailableAllDirectionsCells(requiredDirections);
            }
            return GetRandomCell(availableCells);
        }

        public RotatedLabyrinthCell GetRandomDeadEndCell(List<LabyrinthCell.Direction> requiredDirections) {
            var epoch = _cellsEpochs.Last();
            var availableCells = epoch.GetAvailableOnlyThisDirectionsCells(requiredDirections);
            if(availableCells.Length == 0) {
                availableCells = _allCells.GetAvailableOnlyThisDirectionsCells(requiredDirections);
            }
            return GetRandomCell(availableCells);
        }

        private RotatedLabyrinthCell GetRandomCell(RotatedCellWithWeight[] rotatedCells) {
            int weightSum = 0;
            foreach (var cell in rotatedCells) {
                weightSum += cell.Weight;
            }
            var randomPosition = Random.Range(0, weightSum);
            int searchSum = 0;
            foreach (var cell in rotatedCells) {
                searchSum += cell.Weight;
                if (randomPosition <= searchSum) {
                    return cell.LabyrinthCell;
                }
            }
            return rotatedCells.Last().LabyrinthCell;
        }

        private CellsWithEpoch GetEpoch(int epochNum) {
            foreach (var cellsEpoch in _cellsEpochs) {
                if (cellsEpoch.MaxEpoch > epochNum) {
                    return cellsEpoch;
                }
            }
            return _cellsEpochs.Last();
        }
    }
}

