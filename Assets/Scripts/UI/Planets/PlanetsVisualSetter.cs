using Data;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class PlanetsVisualSetter : MonoBehaviour {

        [SerializeField]
        private PlanetsVisualData _planetsVisualData;

        [SerializeField]
        private Image _planetImage;

        [SerializeField]
        private LocalizationText _planetText;

        [SerializeField]
        private TextMeshProUGUI _planetNameLabel;

        public void UpdatePlanet() {
            var planetIndex = Account.Instance.CurrentStage % _planetsVisualData.Planets.Length;
            _planetImage.sprite = _planetsVisualData.Planets[planetIndex].sprite;
            _planetNameLabel.text = string.Format(_planetText.GetText(), _planetsVisualData.Planets[planetIndex].name.GetText());
        }
    }
}
