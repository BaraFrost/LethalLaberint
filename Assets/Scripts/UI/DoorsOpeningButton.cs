using Game;
using Infrastructure;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class DoorsOpeningButton : MonoBehaviour {

        [SerializeField]
        private Button _button;

        [SerializeField]
        private PlayerInputLogic _playerInputLogic;

        [SerializeField]
        private float _distanceToPlayer;

        [SerializeField]
        private TextMeshProUGUI _buttonLabel;

        [SerializeField]
        private LocalizationText _buttonText;

        private List<LabyrinthCellWithDoor> CellsWithDoors => _entitiesContainer.cellsContainer.CellsWithDoors;

        private LabyrinthCellWithDoor _currentCell;

        private GameEntitiesContainer _entitiesContainer;

        public void Init(GameEntitiesContainer entitiesContainer) {
            _entitiesContainer = entitiesContainer;
            _buttonLabel.text = _buttonText.GetText();
            _button.onClick.AddListener(ChangeDoorState);
            _playerInputLogic.onDoorButtonClicked += ChangeDoorState;
            _button.gameObject.SetActive(false);
        }

        private void Update() {
            if (CellsWithDoors == null || CellsWithDoors.Count == 0) {
                return;
            }
            UpdateCurrentCell();
        }

        private void UpdateCurrentCell() {
            LabyrinthCellWithDoor closestCell = null;
            var distanceToClosestCell = _distanceToPlayer;
            for (int i = 0; i < CellsWithDoors.Count; i++) {
                if(!CellsWithDoors[i].enabled) {
                    continue;
                }
                var distanceToCell = (CellsWithDoors[i].transform.position - _entitiesContainer.playerController.transform.position).sqrMagnitude;
                if (distanceToCell < distanceToClosestCell) {
                    closestCell = CellsWithDoors[i];
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
        }

        private void ChangeDoorState() {
            if (_currentCell == null) {
                return;
            }
            _currentCell.ChangeDoorState();
        }
    }
}
