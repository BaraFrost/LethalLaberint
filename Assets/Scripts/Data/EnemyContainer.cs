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
            private int _count;
            public int Count => _count;

            [SerializeField]
            private int _minStage;
            public int MinStage => _minStage;
        }

        [SerializeField]
        private EnemyWithCount[] _enemies;

        private List<(EnemyWithCount, int)> _cachedEnemyWithCount;

        public int GetEnemySum() {
            var sum = 0;
            foreach(var enemy in _enemies) {
                sum += enemy.Count;
            }
            return sum;
        }

        public void ResetCachedEnemy(float countMult) {
            if(countMult < 1) {
                countMult = 1;
            }
            _cachedEnemyWithCount = _enemies.Select(e => (e, (int)(e.Count * countMult))).ToList();
        }

        public Enemy GetRandomEnemy(float countMult, int stage) {
            if (_cachedEnemyWithCount == null) {
                ResetCachedEnemy(countMult);
            }
            var enemies = _cachedEnemyWithCount.Where(enemy => enemy.Item1.MinStage <= stage).ToArray();
            var randomIndex = UnityEngine.Random.Range(0, enemies.Length);
            var enemyWithCache = enemies[randomIndex];
            enemyWithCache.Item2--;
            if (enemyWithCache.Item2 <= 0) {
                _cachedEnemyWithCount.RemoveAt(randomIndex);
            }
            return enemyWithCache.Item1.Enemy;
        }
    }
}
