using Game;
using TMPro;
using UnityEngine;

namespace UI {

    public class InventoryVisualizer : MonoBehaviour {

        [SerializeField]
        private InventoryItem _buttonPrefab;

        [SerializeField]
        private Transform _contentRoot;

        [SerializeField]
        private SelectAbilityInventory _menuInventory;

        [SerializeField]
        private TextMeshProUGUI _abilityDescriptionText;

        [SerializeField]
        private GameObject _useAbilityArrow;

        private InventoryItem _spawnedButtons;

        private PlayerController _playerController;
        private StartLabyrinthCells _startLabyrinthCells;

        public void Init(PlayerController playerController, StartLabyrinthCells startLabyrinthCells) {
            _startLabyrinthCells = startLabyrinthCells;
            _playerController = playerController;
            _menuInventory.Init();
            _spawnedButtons = Instantiate(_buttonPrefab, _contentRoot);
            _menuInventory.onAbilitySwitched += InitAbilityVisual;
            InitAbilityVisual();
        }

        private void Update() {
            if(TutorialLogic.Instance != null && TutorialLogic.Instance.CanUseAbility) {
                _useAbilityArrow.gameObject.SetActive(true);
            } else if(TutorialLogic.Instance != null) {
                _useAbilityArrow.gameObject.SetActive(false);
            }
            if (_spawnedButtons != null && _playerController.PlayerAbilityLogic.CurrentAbility != null) {
                _spawnedButtons.UpdateProgress(_playerController.PlayerAbilityLogic.CurrentAbility);
            }
        }

        private void InitAbilityVisual() {
            _spawnedButtons.Init(0, _playerController.PlayerAbilityLogic.CurrentAbility);
            _spawnedButtons.onButtonClicked += _playerController.PlayerInputLogic.AbilityButtonActivate;
            var abilityData = _playerController.PlayerAbilityLogic.CurrentAbility.AbilityData;
            _abilityDescriptionText.text = abilityData.AbilityDescription.GetText();
            _playerController.PlayerAbilityLogic.onAbilityActivate += () => _spawnedButtons.UpdateItem(_playerController.PlayerAbilityLogic.CurrentAbility);
        }
    }
}
