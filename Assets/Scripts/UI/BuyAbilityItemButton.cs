using Data;
using TMPro;
using UnityEngine;

namespace UI {

    public class BuyAbilityItemButton : BuyItemButton {

        [SerializeField]
        private TextMeshProUGUI _abilityCounter;
        [SerializeField]
        private int _abilityId;

        [SerializeField]
        private TextMeshProUGUI _abilityName;

        protected override void UpdateMoneyText() {
            base.UpdateMoneyText();
            _abilityName.text = Account.Instance.AbilityDataContainer.GetAbility(_abilityId).AbilityName.GetText();
            _abilityCounter.text = Account.Instance.AbilitiesCountData[_abilityId].ToString();
        }
    }
}
