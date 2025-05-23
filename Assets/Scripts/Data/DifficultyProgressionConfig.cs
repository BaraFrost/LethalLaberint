using NaughtyAttributes;
using System;
using System.Linq;
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

        [Serializable]
        public class DaysByStage {

            [SerializeField]
            private int _stage;
            public int Stage => _stage;

            [SerializeField]
            private int _days;
            public int Days => _days;

            [SerializeField]
            private int _money;
            public int Money => _money;

            [SerializeField]
            private int _requiredAbility;
            public int RequiredAbility => _requiredAbility;
        }

        [SerializeField]
        private int _maxStage;
        public int MaxStage => _maxStage;

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
        public int TotalRequiredMoney => GetMoneyByStage(CurrentDifficultyStage);
        public int RequiredMoneyInDay => TotalRequiredMoney / DaysCount + AdditionalSpawnMoney;

        [SerializeField]
        private MinMaxValue _additionalSpawnMoney;
        public int AdditionalSpawnMoney => (int)GetValue(_additionalSpawnMoney);

        [SerializeField]
        private AnimationCurve _difficultyProgressionCurve;

        private int DaysCount => GetDaysByStage(CurrentDifficultyStage);

        [SerializeField]
        private int _defaultDaysCount;

        [SerializeField]
        private DaysByStage[] _daysByStageOverride;

        public bool IsBonusStage => _daysByStageOverride.FirstOrDefault(data => data.Stage == Account.Instance.CurrentStage) != null;
        public int CurrentDifficultyStage => Math.Min(Account.Instance.CurrentStage, _maxStage);

        public float GetValue(MinMaxValue value) {
            return value.MinValue + value.Difference * _difficultyProgressionCurve.Evaluate((float)CurrentDifficultyStage / _maxStage);
        }

        public int GetDaysByStage(int stage) {
            var overrideDays = _daysByStageOverride.FirstOrDefault(data => data.Stage == stage);
            if (overrideDays != null) {
                return overrideDays.Days;
            }
            return _defaultDaysCount;
        }

        public int GetMoneyByStage(int stage) {
            var overrideDays = _daysByStageOverride.FirstOrDefault(data => data.Stage == stage);
            if (overrideDays != null) {
                return overrideDays.Money;
            }
            return (int)GetValue(_requiredMoney);
        }

        public int RequiredAbility(int stage) {
            var overrideDays = _daysByStageOverride.FirstOrDefault(data => data.Stage == stage);
            if (overrideDays == null) {
                return -1;
            }
            return overrideDays.RequiredAbility;
        }

#if UNITY_EDITOR
        [Button]
        public void DebugLogValues() {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"LabyrinthSize:{LabyrinthSize}");
            stringBuilder.AppendLine($"LabyrinthCellsCount:{LabyrinthCellsCount}");
            stringBuilder.AppendLine($"LabyrinthEpoch:{LabyrinthEpoch}");
            stringBuilder.AppendLine($"EnemyCount:{EnemyCount}");
            stringBuilder.AppendLine($"RequiredMoney:{TotalRequiredMoney}");
            stringBuilder.AppendLine($"RequiredMoneyInDay:{RequiredMoneyInDay}");
            Debug.Log(stringBuilder.ToString());
        }
#endif
    }
}


