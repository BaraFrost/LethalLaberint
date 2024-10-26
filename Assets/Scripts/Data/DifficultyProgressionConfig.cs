using NaughtyAttributes;
using System;
using System.Text;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(DifficultyProgressionConfig), menuName = "Data/DifficultyProgressionConfig")]
    public class DifficultyProgressionConfig : ScriptableObject {

        [Serializable]
        public class MinMaxValue {
            [SerializeField]
            private float _minValue;
            public float MinValue => _minValue;

            [SerializeField]
            private float _maxValue;
            public float MaxValue => _maxValue;

            public float Difference => _maxValue - _minValue;
        }

        [SerializeField]
        private int _maxStage;

        [SerializeField]
        private MinMaxValue _labyrinthSize;
        public int LabyrinthSize => (int)GetValue(_labyrinthSize);

        [SerializeField]
        private MinMaxValue _labyrinthCellsCount;
        public int LabyrinthCellsCount => (int)GetValue(_labyrinthCellsCount);

        [SerializeField]
        private MinMaxValue _labyrinthEpoch;
        public int LabyrinthEpoch => (int)GetValue(_labyrinthEpoch);

        [SerializeField]
        private MinMaxValue _enemyCount;
        public int EnemyCount => (int)GetValue(_enemyCount);

        [SerializeField]
        private MinMaxValue _requiredMoney;
        public int RequiredMoney => (int)GetValue(_requiredMoney);
        public int RequiredMoneyInDay => (int)GetValue(_requiredMoney) / _daysCount;

        [SerializeField]
        private AnimationCurve _difficultyProgressionCurve;

        [SerializeField]
        private int _daysCount;

        private int _currentDifficultyStage;

        public void Init(int currentStage) {
            _currentDifficultyStage = Math.Min(currentStage, _maxStage);
        }

        public float GetValue(MinMaxValue value) {
            return value.MinValue + value.Difference * _difficultyProgressionCurve.Evaluate((float)_currentDifficultyStage / _maxStage);
        }

#if UNITY_EDITOR
        [BoxGroup("Test")]
        [SerializeField]
        private int _logStage;

        [Button]
        public void DebugLogValues() {
            Init(_logStage);
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"LabyrinthSize:{LabyrinthSize}");
            stringBuilder.AppendLine($"LabyrinthCellsCount:{LabyrinthCellsCount}");
            stringBuilder.AppendLine($"LabyrinthEpoch:{LabyrinthEpoch}");
            stringBuilder.AppendLine($"EnemyCount:{EnemyCount}");
            stringBuilder.AppendLine($"RequiredMoney:{RequiredMoney}");
            stringBuilder.AppendLine($"RequiredMoneyInDay:{RequiredMoneyInDay}");
            Debug.Log(stringBuilder.ToString());
        }
#endif
    }
}


