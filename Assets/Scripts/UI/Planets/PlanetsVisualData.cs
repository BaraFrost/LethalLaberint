using Infrastructure;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI {

    [CreateAssetMenu(fileName = nameof(PlanetsVisualData), menuName = "Data/PlanetsVisualData")]
    public class PlanetsVisualData : ScriptableObject {

        [Serializable]
        public struct PlanetData {
            public AssetReference spriteAssetReference;
            public LocalizationText name;
        }

        [SerializeField]
        private PlanetData[] _planetsData;
        public PlanetData[] Planets => _planetsData;
    }
}

