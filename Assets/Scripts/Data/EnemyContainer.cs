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
            private int _minStage;
            public int MinStage => _minStage;
        }

        [SerializeField]
        private EnemyWithCount[] _enemies;

        public List<Enemy> GetRandomEnemies(float weight, int stage) {
            var result = new List<Enemy>();
            var enemiesByStage = _enemies.Where(enemy => enemy.MinStage <= stage).ToArray();
            var availableEnemies = enemiesByStage.Where(enemy => enemy.Weight <= weight).ToArray();
            while (availableEnemies.Length > 0) {
                var randomEnemy = availableEnemies[UnityEngine.Random.Range(0, availableEnemies.Length)];
                result.Add(randomEnemy.Enemy);
                weight -= randomEnemy.Weight;
                availableEnemies = enemiesByStage.Where(enemy => enemy.Weight <= weight).ToArray();
            }
            return result;
        }
    }
}
