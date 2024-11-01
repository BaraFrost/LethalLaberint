using TMPro;
using UnityEngine;

namespace Game {
    public class StageVisualizer : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _text;

        private void Update() {
            _text.text = StageCounter.Instance.CurrentStage.ToString();
        }

    }
}

