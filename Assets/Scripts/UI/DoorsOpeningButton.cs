using Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class DoorsOpeningButton : MonoBehaviour {

        [SerializeField]
        private Button _button;

        /* [SerializeField]
         private Camera _gameCamera;*/

        [SerializeField]
        private PlayerInputLogic _playerInputLogic;

        [SerializeField]
        private float _distanceToPlayer;

        private List<LabyrinthCellWithDoor> _cellsWithDoors;

        private LabyrinthCellWithDoor _currentCell;

        private Transform _playerTransform;

        public void Init(List<LabyrinthCellWithDoor> cellsWithDoors, Transform playerTransform) {
            _playerTransform = playerTransform;
            _cellsWithDoors = cellsWithDoors;
            _button.onClick.AddListener(ChangeDoorState);
            _playerInputLogic.onDoorButtonClicked += ChangeDoorState;
            _button.gameObject.SetActive(false);
        }

        private void Update() {
            if (_cellsWithDoors == null || _cellsWithDoors.Count == 0) {
                return;
            }
            UpdateCurrentCell();
        }

        private void UpdateCurrentCell() {
            LabyrinthCellWithDoor closestCell = null;
            var distanceToClosestCell = _distanceToPlayer;
            for (int i = 0; i < _cellsWithDoors.Count; i++) {
                var distanceToCell = (_cellsWithDoors[i].transform.position - _playerTransform.position).sqrMagnitude;
                if (distanceToCell < distanceToClosestCell) {
                    closestCell = _cellsWithDoors[i];
                    distanceToClosestCell = distanceToCell;
                }
            }
            if (_currentCell != closestCell) {
                if (_currentCell != null) {
                    _currentCell.DeselectDoor();
                }
                if(closestCell != null) {
                    closestCell.SelectDoor();
                }
            }
            _currentCell = closestCell;

            if (_currentCell == null) {
                _button.gameObject.SetActive(false);
            } else {
                _button.gameObject.SetActive(true);
            }
            /*
            var closestCellScreenPosition = _gameCamera.WorldToScreenPoint(closestCell.transform.position);
            var isOffScreen = closestCellScreenPosition.z < 0 || closestCellScreenPosition.x < 0 || 
                closestCellScreenPosition.x > Screen.width || closestCellScreenPosition.y < 0 || 
                closestCellScreenPosition.y > Screen.height;
            if (isOffScreen) {
                _currentCell = null;
                _button.gameObject.SetActive(false);
            }
            else {
                _currentCell = closestCell;
                _button.gameObject.SetActive(true);
            }*/
        }

        private void ChangeDoorState() {
            if (_currentCell == null) {
                return;
            }
            _currentCell.ChangeDoorState();
        }
    }
}
