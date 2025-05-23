using Data;
using Infrastructure;
using TMPro;
using UnityEngine;

namespace MiniGames.FlappyBird {

    public class FlappyBirdWalletVisualizer : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private TextMeshProUGUI _recordLabel;
        [SerializeField]
        private LocalizationText _recordText;

        [SerializeField]
        private FlappyBirdWallet _wallet;

        private void Start() {
            _wallet._onValueUpdate += UpdateText;
            UpdateText();
        }

        private void UpdateText() {
            var bestScore = Account.Instance.MiniGameRecord > _wallet.Value ? Account.Instance.MiniGameRecord : _wallet.Value;
            _text.text = _wallet.Value.ToString() + "$";
            _recordLabel.text = _recordText.GetTextFormatted(bestScore.ToString());
        }
    }
}
