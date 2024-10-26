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
        }

        [SerializeField]
        private EnemyWithCount[] _enemies;

        private List<(Enemy, int)> _cachedEnemyWithCount;

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
            _cachedEnemyWithCount = _enemies.Select(e => (e.Enemy, (int)(e.Count * countMult))).ToList();
        }

        public Enemy GetRandomEnemy(float countMult) {
            if (_cachedEnemyWithCount == null) {
                ResetCachedEnemy(countMult);
            }
            var randomIndex = UnityEngine.Random.Range(0, _cachedEnemyWithCount.Count);
            var enemyWithCache = _cachedEnemyWithCount[randomIndex];
            enemyWithCache.Item2--;
            if (enemyWithCache.Item2 <= 0) {
                _cachedEnemyWithCount.RemoveAt(randomIndex);
            }
            return enemyWithCache.Item1;
        }
    }
}
