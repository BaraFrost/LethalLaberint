using Data;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Game {

    public class MenuInventory : MonoBehaviour {

        [SerializeField]
        private InventoryMenuItem _abilityItemPrefab;
        private List<InventoryMenuItem> _abilityItems = new List<InventoryMenuItem>();

        public void Init() {
            var abilityIds = Account.Instance.AbilityDataContainer.GetAllAbilityIds();
            foreach (var abilityId in abilityIds) {
                var abilityItem = Instantiate(_abilityItemPrefab, transform);
                abilityItem.onButtonClicked += TryToSelectAbility;
                abilityItem.Init(abilityId);
                if (abilityId == Account.Instance.CurrentAbilityId) {
                    abilityItem.Select();
                }
                _abilityItems.Add(abilityItem);
            }
            if (Account.Instance.AbilitiesCountData[Account.Instance.CurrentAbilityId] <= 0) {
                var availableAbilityIds = Account.Instance.AbilitiesCountData.Where(countData => countData.Value > 0).ToArray();
                if (availableAbilityIds.Length > 0) {
                    var inventoryItem = _abilityItems.First(item => item.AbilityId == availableAbilityIds[0].Key);
                    TryToSelectAbility(inventoryItem);
                }
            }
        }

        private void TryToSelectAbility(InventoryMenuItem inventoryMenuItem) {
            if (Account.Instance.AbilitiesCountData[inventoryMenuItem.AbilityId] <= 0) {
                return;
            }
            if (Account.Instance.TryToSelectAbility(inventoryMenuItem.AbilityId)) {
                DeselectAll();
                inventoryMenuItem.Select();
            }
        }

        private void DeselectAll() {
            foreach (var item in _abilityItems) {
                item.Deselect();
            }
        }
    }
}
