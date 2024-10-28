using Game;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(StartCellsContainer), menuName = "Data/StartCellsContainer")]
    public class StartCellsContainer : ScriptableObject {

        [SerializeField]
        private StartLabyrinthCells[] _startLabyrinthCells;

        public StartLabyrinthCells GetRandomStartCells() {
            return _startLabyrinthCells[Random.Range(0, _startLabyrinthCells.Length)];
        }
    }
}
