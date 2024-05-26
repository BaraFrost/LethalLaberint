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
        private class CellsEpoch {

            [SerializeField]
            private CellWithWeight[] _labyrinthCells;
            public CellWithWeight[] LabyrinthCells => _labyrinthCells;

            public List<RotatedCellWithWeight> RotatedCellsWithWeight { get; private set; }

            [SerializeField]
            private int _maxEpoch;
            public int MaxEpoch => _maxEpoch;

            public void GenerateRotatedCells() {
                if (RotatedCellsWithWeight != null) {
                    return;
                }
                RotatedCellsWithWeight = new List<RotatedCellWithWeight>();
                foreach (var prefabWithWeight in _labyrinthCells) {
                    for (int i = 0; i < 4; i++) {
                        var rotatedLabyrinthCell = new RotatedLabyrinthCell(prefabWithWeight.LabyrinthCell, 90 * i);
                        RotatedCellsWithWeight.Add(new RotatedCellWithWeight(rotatedLabyrinthCell, prefabWithWeight.Weight));
                    }
                }
            }
        }

        [SerializeField]
        private CellsEpoch[] _cellsEpochs;

        public RotatedLabyrinthCell GetRandomCell(List<LabyrinthCell.Direction> requiredDirections, int epochNum) {
            var epoch = GetEpoch(epochNum);
            epoch.GenerateRotatedCells();
            var availableCells = epoch.RotatedCellsWithWeight.Where(cell => cell.LabyrinthCell.AvailableAllDirections(requiredDirections)).ToArray();
            return GetRandomCell(availableCells);
        }

        public RotatedLabyrinthCell GetRandomDeadEndCell(List<LabyrinthCell.Direction> requiredDirections) {
            var epoch = _cellsEpochs.Last();
            epoch.GenerateRotatedCells();
            var availableCells = epoch.RotatedCellsWithWeight.Where(cell => cell.LabyrinthCell.AvailableOnlyThisDirections(requiredDirections)).ToArray();
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

        private CellsEpoch GetEpoch(int epochNum) {
            foreach (var cellsEpoch in _cellsEpochs) {
                if (cellsEpoch.MaxEpoch > epochNum) {
                    return cellsEpoch;
                }
            }
            return _cellsEpochs.Last();
        }
    }
}

