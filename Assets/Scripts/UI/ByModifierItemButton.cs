using Data;
using Infrastructure;
using TMPro;
using UnityEngine;

namespace UI {

    public class ByModifierItemButton : BuyItemButton {

        [SerializeField]
        private TextMeshProUGUI _modifierLevelText;

        [SerializeField]
        private ModifierType _modifierType;

        [SerializeField]
        private TextMeshProUGUI _nameLabel;

        [SerializeField]
        private LocalizationText _nameText;

        protected override void UpdateMoneyText() {
            base.UpdateMoneyText();
            _modifierLevelText.text = $"+{Account.Instance.LevelsModifiersContainer.Modifiers[_modifierType].Value * 100}%";// Account.Instance.ModifiersCountData[_modifierType].ToString();
            _nameLabel.text = _nameText.GetText();
        }
    }
}
