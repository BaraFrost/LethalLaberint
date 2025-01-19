using Data;
using TMPro;
using UnityEngine;

namespace UI {

    public class BuyAbilityItemButton : BuyItemButton {

        [SerializeField]
        private TextMeshProUGUI _abilityCounter;
        [SerializeField]
        private int _abilityId;

        protected override void UpdateMoneyText() {
            base.UpdateMoneyText();
            _abilityCounter.text = Account.Instance.AbilitiesCountData[_abilityId].ToString();
        }
    }
}
