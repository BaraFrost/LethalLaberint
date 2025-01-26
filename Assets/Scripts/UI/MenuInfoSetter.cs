using Data;
using Infrastructure;
using TMPro;
using UnityEngine;

namespace Game {

    public class MenuInfoSetter : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _stageLabel;
        [SerializeField]
        private LocalizationText _stageText;
        [SerializeField]
        private TextMeshProUGUI _dayLabel;
        [SerializeField]
        private LocalizationText _dayText;
        [SerializeField]
        private TextMeshProUGUI _moneyLabel;
        [SerializeField]
        private LocalizationText _moneyText;
        [SerializeField]
        private TextMeshProUGUI _quotaLabel;
        [SerializeField]
        private LocalizationText _quotaText;

        public void UpdateInfo() {
            _stageLabel.text = _stageText.GetTextFormatted(Account.Instance.CurrentStage.ToString());
            _dayLabel.text = _dayText.GetTextFormatted(Account.Instance.CurrentDay.ToString(), Account.Instance.TotalDays.ToString());
            _moneyLabel.text = _moneyText.GetTextFormatted(Account.Instance.TotalMoney.ToString());
            _quotaLabel.text = _quotaText.GetTextFormatted(Account.Instance.CurrentStageMoney.ToString(), Account.Instance.RequiredMoney.ToString());
        }
    }
}
