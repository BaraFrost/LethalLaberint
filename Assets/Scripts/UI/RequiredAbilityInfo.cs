using Data;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RequiredAbilityInfo : MonoBehaviour {

        [SerializeField]
        private LocalizationText _requiredAbilityText;

        [SerializeField]
        private TextMeshProUGUI _label;

        [SerializeField]
        private Image _abilityImage;

        public void Init() {
            var abilityId = Account.Instance.DifficultyProgressionConfig.RequiredAbility(Account.Instance.CurrentStage);
            if (abilityId <= 0) {
                _abilityImage.gameObject.SetActive(false);
                _label.gameObject.SetActive(false);
                return;
            }
            ;
            //_abilityImage.sprite = Account.Instance.AbilityDataContainer.GetAbility(abilityId).Sprite;
            _label.text = _requiredAbilityText.GetText();
        }
    }
}

