using Data;
using Infrastructure;
using TMPro;
using UnityEngine;

namespace Game {

    public class MoneyWallet : AbstractPlayerLogic {

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private LocalizationText _moneyText;

        private int _moneyCount;
        public int MoneyCount => _moneyCount;

        public override void Init(PlayerController player) {
            base.Init(player);
            UpdateMoneyText();
        }

        public void AddMoney(int count) {
            _moneyCount += count;
            UpdateMoneyText();
        }

        private void UpdateMoneyText() {
            var requiredMoney = Mathf.Max(0, Account.Instance.RequiredMoney - Account.Instance.CurrentStageMoney - _moneyCount);
            _text.text = string.Format(_moneyText.GetText(), _moneyCount, requiredMoney, Account.Instance.TotalDays - Account.Instance.CurrentDay);
        }

    }
}
