using Game;
using System;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(EnvironmentContainer), menuName = "Data/EnvironmentContainer")]
    public class EnvironmentContainer : ScriptableObject {

        [Serializable]
        public struct CellsEnvironmentWithPercent {
            public float percent;
            public CellEnvironment environment;
        }

        [SerializeField]
        private float _maxEnvironmentPercent;
        public float MaxEnvironmentPercent => _maxEnvironmentPercent;

        [SerializeField]
        private CellsEnvironmentWithPercent[] _cellsEnvironments;
        public CellsEnvironmentWithPercent[] CellsEnvironments => _cellsEnvironments;
    }
}

