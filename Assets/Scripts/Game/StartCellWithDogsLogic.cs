using UnityEngine;

namespace Game {

    public class StartCellWithDogsLogic : MonoBehaviour {

        [SerializeField]
        private PlayerStayWatcher _playerStayWatcher;

        [SerializeField]
        private LabyrinthCellWithDoor[] _doors;

        private void Start() {
            _playerStayWatcher.onPlayerEnter += OpenAllDoors;
        }

        private void OpenAllDoors() {
            foreach (var door in _doors) {
                if(!door.IsOpened) {
                    door.ChangeDoorState();
                }
            }
        }
    }
}

