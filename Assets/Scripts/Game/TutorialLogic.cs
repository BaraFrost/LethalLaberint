using Data;
using NaughtyAttributes;
using System;
using UI;
using UnityEngine;

namespace Game {

    public class TutorialLogic : MonoBehaviour {

        public static TutorialLogic Instance { get; private set; }

        private enum TutorialState {
            SelectAbility,
            Start,
            TakeItem,
            OpenDoor,
            CatchBug,
            UseAbility,
            CellItems,
            Exit,
        }

        [BoxGroup("SelectAbility")]
        [SerializeField]
        private GameObject _selectAbilityGroup;

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

        [SerializeField]
        [BoxGroup("TakeItem")]
        private AbstractPopup _bagPopup;

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
        private GameObject _exitGroup;

        [BoxGroup("Explode")]
        [SerializeField]
        private PlayerStayWatcher _explodeTrigger;
        [BoxGroup("Explode")]
        [SerializeField]
        private GameObject _explodeGroup;

        [BoxGroup("UseAbility")]
        [SerializeField]
        private GameObject _useAbilityGroup;

        [SerializeField]
        private int _tutorialAbilityId;
        public int TutorialAbilityId => _tutorialAbilityId;

        public Action OnAbilitySelected;
        public Action OnAbilityUsed;

        public bool CanMove => _currentState != TutorialState.SelectAbility;
        public bool CanUseAbility => _currentState == TutorialState.UseAbility;

        private TutorialState _currentState;

        private void Awake() {
            Instance = this;
            if (Account.Instance.AbilitiesCountData[_tutorialAbilityId] == 0) {
                Account.Instance.AddAbility(_tutorialAbilityId);
            }
            OnAbilitySelected += () => TurnNextState(TutorialState.Start);
            _startTrigger.onPlayerEnter += () => TurnNextState(TutorialState.TakeItem);
            _takeItemTrigger.onPlayerEnter += () => { _bagPopup.OnHided += () => TurnNextState(TutorialState.OpenDoor); _bagPopup.ShowPopup(); };
            _door.onDoorStateChanged += () => TurnNextState(TutorialState.CatchBug);
            _catchTrigger.onPlayerEnter += () => TurnNextState(TutorialState.UseAbility);
            OnAbilityUsed += () => TurnNextState(TutorialState.CellItems);
            _cellTrigger.onPlayerEnter += () => TurnNextState(TutorialState.Exit);
            _explodeGroup.gameObject.SetActive(false);
            _explodeTrigger.onPlayerEnter += () => _explodeGroup.gameObject.SetActive(true);
            TurnNextState(TutorialState.SelectAbility);
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
            _exitGroup.SetActive(false);
            _takeItemGroup.SetActive(false);
            _doorText.SetActive(false);
            _startGroup.SetActive(false);
            _selectAbilityGroup.SetActive(false);
            _useAbilityGroup.SetActive(false);
        }

        private void TurnNextState(TutorialState state) {
            _currentState = state;
            switch (_currentState) {
                case TutorialState.SelectAbility:
                    ResetLogic();
                    _selectAbilityGroup.SetActive(true);
                    break;
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
                case TutorialState.UseAbility:
                    ResetLogic();
                    _useAbilityGroup.SetActive(true);
                    break;
                case TutorialState.CellItems:
                    ResetLogic();
                    _cellText2.SetActive(true);
                    _cellText.SetActive(true);
                    _cellTrigger.IsActive = true;
                    break;
                case TutorialState.Exit:
                    ResetLogic();
                    _exitGroup.SetActive(true);
                    break;
            }
        }
    }
}

