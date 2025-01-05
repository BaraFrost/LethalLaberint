using TMPro;
using UnityEngine;

namespace MiniGames.FlappyBird {

    public class FlappyBirdWalletVisualizer : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private FlappyBirdWallet _wallet;

        private void Start() {
            _wallet._onValueUpdate += UpdateText;
            UpdateText();
        }

        private void UpdateText() {
            _text.text = _wallet.Value.ToString() + "$";
        }
    }
}
