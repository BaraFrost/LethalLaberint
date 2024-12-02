using Game;
using System;
using System.Linq;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(StartCellsContainer), menuName = "Data/StartCellsContainer")]
    public class StartCellsContainer : ScriptableObject {

        [Serializable]
        private class StartLabyrinthCellsWithMinStage {
            public StartLabyrinthCells startLabyrinthCells;
            public int minStage;
        }

        [SerializeField]
        private StartLabyrinthCellsWithMinStage[] _startLabyrinthCells;

        [SerializeField]
        private StartLabyrinthCellsWithMinStage[] _bonusLabyrinthCells;

        public StartLabyrinthCells GetRandomStartCells(int currentStage) {
            var bonusCell = _bonusLabyrinthCells.FirstOrDefault(cell => cell.minStage == currentStage);
            if(bonusCell != null) {
                return bonusCell.startLabyrinthCells;
            }
            var startCells = _startLabyrinthCells.Where(cell => cell.minStage <= currentStage).ToArray(); 
            return startCells[UnityEngine.Random.Range(0, startCells.Length)].startLabyrinthCells;
        }
    }
}
