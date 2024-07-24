using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game {

    public class SpawnedLabyrinthCellsContainer {

        private List<LabyrinthCell> _cells;
        private List<LabyrinthCell> _availableCells;
        public List<LabyrinthCell> AvailableCells => _availableCells;
        public LabyrinthCell[,] Field { get; private set; }
        public int LabyrinthSize { get; private set; }

        public SpawnedLabyrinthCellsContainer(List<LabyrinthCell> cells, int labyrinthSize, LabyrinthCell[,] field) {
            Field = field;
            _cells = cells;
            _availableCells = cells.Where(cell => !cell.ClosedCell).ToList();
            LabyrinthSize = labyrinthSize;
        }

        public Vector3 GetRandomCellPosition() {
            var randomIndex = Random.Range(0, _availableCells.Count);
            return _availableCells[randomIndex].transform.position;
        }
    }
}
