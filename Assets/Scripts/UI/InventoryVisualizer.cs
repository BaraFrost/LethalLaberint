using Game;
using UnityEngine;

namespace UI {

    public class InventoryVisualizer : MonoBehaviour {

        [SerializeField]
        private InventoryItem _buttonPrefab;

        [SerializeField]
        private Transform _contentRoot;

        [SerializeField]
        private SelectAbilityInventory _menuInventory;

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
            /*
            var spawnedAbilities = _playerAbilityLogic.Abilities;
            for (int i = 0; i < spawnedAbilities.Length; i++) {
                var spawnedButton = Instantiate(_buttonPrefab, _contentRoot);
                var spawnedAbility = spawnedAbilities[i];
                spawnedButton.Init(i, spawnedAbility);
                spawnedButton.onButtonClicked += _playerInputLogic.AbilityButtonActivate;
                _playerAbilityLogic.onAbilityActivate += () => spawnedButton.UpdateItem(spawnedAbility);
                _spawnedButtons.Add(spawnedButton);
            }*/
        }

        private void Update() {
            _menuInventory.gameObject.SetActive(_startLabyrinthCells.ShipLogic.PositionInsideShip(_playerController.transform.position));
        }

        private void InitAbilityVisual() {
            _spawnedButtons.Init(0, _playerController.PlayerAbilityLogic.CurrentAbility);
            _spawnedButtons.onButtonClicked += _playerController.PlayerInputLogic.AbilityButtonActivate;
            _playerController.PlayerAbilityLogic.onAbilityActivate += () => _spawnedButtons.UpdateItem(_playerController.PlayerAbilityLogic.CurrentAbility);
        }
    }
}
