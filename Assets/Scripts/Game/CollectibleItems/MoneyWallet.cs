using TMPro;
using UnityEngine;

namespace Game {

    public class MoneyWallet : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _text;

        private int _moneyCount;

        public void AddMoney(int count) {
            _moneyCount += count;
            _text.text = $"Money:{_moneyCount}" ;
        }

    }
}
