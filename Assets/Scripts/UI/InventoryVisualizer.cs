using Game;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class InventoryVisualizer : MonoBehaviour {

        [SerializeField]
        private InventoryItem _buttonPrefab;

        [SerializeField]
        private Transform _contentRoot;

        private List<InventoryItem> _spawnedButtons = new List<InventoryItem>();

        private PlayerAbilityLogic _playerAbilityLogic;
        private PlayerInputLogic _playerInputLogic;

        public void Init(PlayerController playerController) {
            _playerAbilityLogic = playerController.PlayerAbilityLogic;
            _playerInputLogic = playerController.PlayerInputLogic;
            var spawnedAbilities = _playerAbilityLogic.Abilities;
            for (int i = 0; i < spawnedAbilities.Length; i++) {
                var spawnedButton = Instantiate(_buttonPrefab, _contentRoot);
                var spawnedAbility = spawnedAbilities[i];
                spawnedButton.Init(i, spawnedAbility);
                spawnedButton.onButtonClicked += _playerInputLogic.AbilityButtonActivate;
                _playerAbilityLogic.onAbilityActivate += () => spawnedButton.UpdateItem(spawnedAbility);
                _spawnedButtons.Add(spawnedButton);
            }
        }
    }
}
