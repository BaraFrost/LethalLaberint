using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(EnemyContainer), menuName = "Data/EnemyContainer")]
    public class EnemyContainer : ScriptableObject {

        [Serializable]
        private class EnemyWithCount {

            [SerializeField]
            private Enemy _enemy;
            public Enemy Enemy => _enemy;

            [SerializeField]
            private float _weight;
            public float Weight => _weight;

            [SerializeField]
            private AnimationCurve _probabilityByStage;
            public AnimationCurve ProbabilityByStage => _probabilityByStage;

            [SerializeField]
            private int _minStage;
            public int MinStage => _minStage;
        }

        [SerializeField]
        private EnemyWithCount[] _enemies;

        public List<Enemy> GetRandomEnemies(float weight, int stage) {
            var result = new List<Enemy>();
            var availableEnemies = GetAvailableEnemies(weight, stage);
            while (availableEnemies.Length > 0) {
                var randomEnemy = GetRandomEnemy(availableEnemies);
                result.Add(randomEnemy.Enemy);
                weight -= randomEnemy.Weight;
                availableEnemies = GetAvailableEnemies(weight, stage);
            }
            return result;
        }

        private EnemyWithCount[] GetAvailableEnemies(float weight, int stage) {
            var stagePercent = Account.Instance.DifficultyProgressionConfig.CurrentDifficultyStage;
            var enemiesByStage = _enemies.Where(enemy => enemy.MinStage <= stage
            && enemy.ProbabilityByStage.Evaluate(stagePercent) > 0).ToArray();
            return enemiesByStage.Where(enemy => enemy.Weight <= weight).ToArray();
        }

        private EnemyWithCount GetRandomEnemy(EnemyWithCount[] enemies) {
            var stagePercent = Account.Instance.DifficultyProgressionConfig.CurrentDifficultyStage;
            var probabilitySum = 0f;
            foreach (var enemy in enemies) {
                probabilitySum += enemy.ProbabilityByStage.Evaluate(stagePercent);
            }
            var randomValue = UnityEngine.Random.Range(0, probabilitySum);
            var currentValue = 0f;
            foreach (var enemy in enemies) {
                currentValue += enemy.ProbabilityByStage.Evaluate(stagePercent);
                if (randomValue <= currentValue) {
                    return enemy;
                }
            }
            return enemies[0];
        }
    }
}
