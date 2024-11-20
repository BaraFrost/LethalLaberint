using NaughtyAttributes;
using UnityEngine;

namespace Game {

    public class TutorialLogic : MonoBehaviour {

        private enum TutorialState {
            Start,
            TakeItem,
            OpenDoor,
            CatchBug,
            CellItems,
            Exit,
        }

        [BoxGroup("Start")]
        [SerializeField]
        private GameObject _startGroup;
        [BoxGroup("Start")]
        [SerializeField]
        private PlayerStayWatcher _startTrigger;

        [BoxGroup("TakeItem")]
        [SerializeField]
        private GameObject _takeItemGroup;
        [BoxGroup("TakeItem")]
        [SerializeField]
        private PlayerStayWatcher _takeItemTrigger;

        [BoxGroup("Door")]
        [SerializeField]
        private GameObject _doorText;
        [BoxGroup("Door")]
        [SerializeField]
        private LabyrinthCellWithDoor _door;

        [BoxGroup("Bug")]
        [SerializeField]
        private GameObject _catchText;
        [BoxGroup("Bug")]
        [SerializeField]
        private PlayerStayWatcher _catchTrigger;

        [BoxGroup("Cell")]
        [SerializeField]
        private GameObject _cellText;
        [BoxGroup("Cell")]
        [SerializeField]
        private GameObject _cellText2;
        [BoxGroup("Cell")]
        [SerializeField]
        private PlayerStayWatcher _cellTrigger;

        [BoxGroup("Exit")]
        [SerializeField]
        private GameObject _exitText;

        private TutorialState _currentState;

        private void Start() {
            _startTrigger.onPlayerEnter += () => TurnNextState(TutorialState.TakeItem);
            _takeItemTrigger.onPlayerEnter += () => TurnNextState(TutorialState.OpenDoor);
            _door.onDoorStateChanged += () => TurnNextState(TutorialState.CatchBug);
            _catchTrigger.onPlayerEnter += () => TurnNextState(TutorialState.CellItems);
            _cellTrigger.onPlayerEnter += () => TurnNextState(TutorialState.Exit);
            TurnNextState(TutorialState.Start);
        }

        private void ResetLogic() {
            _startTrigger.IsActive = false;
            _door.enabled = false;
            _cellTrigger.IsActive = false;
            _catchTrigger.IsActive = false;
            _takeItemTrigger.IsActive = false;
            _catchText.SetActive(false);
            _cellText2.SetActive(false);
            _cellText.SetActive(false);
            _exitText.SetActive(false);
            _takeItemGroup.SetActive(false);
            _doorText.SetActive(false);
            _startGroup.SetActive(false);
        }

        private void TurnNextState(TutorialState state) {
            _currentState = state;
            switch (_currentState) {
                case TutorialState.Start:
                    ResetLogic();
                    _startGroup.SetActive(true);
                    _startTrigger.IsActive = true;
                    break;
                case TutorialState.TakeItem:
                    ResetLogic();
                    _takeItemTrigger.IsActive = true;
                    _takeItemGroup.SetActive(true);
                    break;
                case TutorialState.OpenDoor:
                    ResetLogic();
                    _door.enabled = true;
                    _doorText.SetActive(true);
                    break;
                case TutorialState.CatchBug:
                    ResetLogic();
                    _catchTrigger.IsActive = true;
                    _catchText.SetActive(true);
                    break;
                case TutorialState.CellItems:
                    ResetLogic();
                    _cellText2.SetActive(true);
                    _cellText.SetActive(true);
                    _cellTrigger.IsActive = true;
                    break;
                case TutorialState.Exit:
                    ResetLogic();
                    _exitText.SetActive(true);
                    break;
            }
        }
    }
}

