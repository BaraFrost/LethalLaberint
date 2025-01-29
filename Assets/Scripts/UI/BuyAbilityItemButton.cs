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
        private GameObject _alertImage;

        protected override void UpdateMoneyText() {
            base.UpdateMoneyText();
            var requiredAbilityId = Account.Instance.DifficultyProgressionConfig.RequiredAbility(Account.Instance.CurrentStage);
            _alertImage.gameObject.SetActive(requiredAbilityId == _abilityId);
            _abilityCounter.text = Account.Instance.AbilitiesCountData[_abilityId].ToString();
        }
    }
}
