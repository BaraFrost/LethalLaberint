using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Game {

    public class SelectAbilityInventory : MonoBehaviour {

        [SerializeField]
        private InventoryAbilityItem _abilityItemPrefab;
        private List<InventoryAbilityItem> _abilityItems = new List<InventoryAbilityItem>();

        public Action onAbilitySwitched;

        private bool IsTutorial => TutorialLogic.Instance != null;

        public void Init() {

            var abilityIds = Account.Instance.AbilityDataContainer.GetAllAbilityIds();
            foreach (var abilityId in abilityIds) {
                var abilityItem = Instantiate(_abilityItemPrefab, transform);
                abilityItem.onButtonClicked += SelectAbility;
                if (IsTutorial) {
                    if (abilityId == TutorialLogic.Instance.TutorialAbilityId) {
                        abilityItem.Init(abilityId, canSelectOnlyThis: true);
                    } else {
                        abilityItem.Init(abilityId, cantSelect: true);
                    }
                } else {
                    abilityItem.Init(abilityId);
                }
                if (abilityId == Account.Instance.CurrentAbilityId) {
                    abilityItem.Select();
                }
                _abilityItems.Add(abilityItem);
            }
            UpdateSelectedAbility();
        }

        private void Update() {
            // UpdateSelectedAbility();
        }

        private void UpdateSelectedAbility() {
            if (Account.Instance.AbilitiesCountData[Account.Instance.CurrentAbilityId] <= 0) {
                var availableAbilityIds = Account.Instance.AbilitiesCountData.Where(countData => countData.Value > 0).ToArray();
                if (availableAbilityIds.Length > 0) {
                    var inventoryItem = _abilityItems.First(item => item.AbilityId == availableAbilityIds[0].Key);
                    if (TryToSelectAbility(inventoryItem)) {
                    }
                }
            }
        }

        private void SelectAbility(InventoryAbilityItem inventoryMenuItem) {
            TryToSelectAbility(inventoryMenuItem);
        }

        private bool TryToSelectAbility(InventoryAbilityItem inventoryMenuItem) {
            if (IsTutorial && inventoryMenuItem.AbilityId != TutorialLogic.Instance.TutorialAbilityId) {
                return false;
            }
            if (Account.Instance.AbilitiesCountData[inventoryMenuItem.AbilityId] <= 0) {
                return false;
            }
            if (Account.Instance.TryToSelectAbility(inventoryMenuItem.AbilityId)) {
                DeselectAll();
                inventoryMenuItem.Select();
                onAbilitySwitched?.Invoke();
                if(IsTutorial) {
                    TutorialLogic.Instance.OnAbilitySelected?.Invoke();
                }
                return true;
            }
            return false;
        }

        private void DeselectAll() {
            foreach (var item in _abilityItems) {
                item.Deselect();
            }
        }
    }
}
