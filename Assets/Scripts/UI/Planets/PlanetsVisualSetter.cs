using Data;
using Infrastructure;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class PlanetsVisualSetter : MonoBehaviour {

        [SerializeField]
        private PlanetsVisualData _planetsVisualData;

        [SerializeField]
        private AsyncImage _planetAsyncImage;

        [SerializeField]
        private LocalizationText _planetText;

        [SerializeField]
        private TextMeshProUGUI _planetNameLabel;

        public void UpdatePlanet() {
            var planetIndex = Account.Instance.CurrentStage % _planetsVisualData.Planets.Length;
            var planetsNameRandom = new System.Random(235 + Account.Instance.CurrentStage);
            var randomLetter = (char)planetsNameRandom.Next(65, 90);
            var randomNumber = planetsNameRandom.Next(100, 1000);
            _planetAsyncImage.Load(_planetsVisualData.Planets[planetIndex].spriteAssetReference);
            _planetNameLabel.text = string.Format(_planetText.GetText(), $"{randomLetter}{randomNumber}");
        }
    }
}
