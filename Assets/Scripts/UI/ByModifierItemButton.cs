using Data;
using TMPro;
using UnityEngine;

namespace UI {

    public class ByModifierItemButton : BuyItemButton {

        [SerializeField]
        private TextMeshProUGUI _modifierLevelText;

        [SerializeField]
        private ModifierType _modifierType;

        [SerializeField]
        private TextMeshProUGUI _modifierValueText;

        protected override void UpdateMoneyText() {
            base.UpdateMoneyText();
            _modifierLevelText.text = Account.Instance.ModifiersCountData[_modifierType].ToString();
            _modifierValueText.text = $"+{Account.Instance.LevelsModifiersContainer.Modifiers[_modifierType].IncreasePerLevelValue * 100}%"; 
        }
    }
}
