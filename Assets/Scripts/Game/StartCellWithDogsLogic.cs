using UnityEngine;

namespace Game {

    public class StartCellWithDogsLogic : MonoBehaviour {

        [SerializeField]
        private PlayerStayWatcher _playerStayWatcher;

        [SerializeField]
        private LabyrinthCellWithDoor[] _doors;

        private bool _opened;

        private void Start() {
            _playerStayWatcher.onPlayerEnter += OpenAllDoors;
        }

        private void OpenAllDoors() {
            if(_opened) {
                return;
            }
            foreach (var door in _doors) {
                _opened = true;
                if (!door.IsOpened) {
                    door.ChangeDoorState();
                }
            }
        }
    }
}

