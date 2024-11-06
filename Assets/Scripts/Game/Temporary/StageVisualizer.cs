using Data;
using TMPro;
using UnityEngine;

namespace Game {
    public class StageVisualizer : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _text;

        private void Update() {
            _text.text = Account.Instance.CurrentStage.ToString();
        }
    }
}

