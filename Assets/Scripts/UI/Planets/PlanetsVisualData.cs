using Infrastructure;
using System;
using UnityEngine;

namespace UI {

    [CreateAssetMenu(fileName = nameof(PlanetsVisualData), menuName = "Data/PlanetsVisualData")]
    public class PlanetsVisualData : ScriptableObject {

        [Serializable]
        public struct PlanetData {
            public Sprite sprite;
            public LocalizationText name;
        }

        [SerializeField]
        private PlanetData[] _planetsData;
        public PlanetData[] Planets => _planetsData;
    }
}

