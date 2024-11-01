using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class SpawnAdditionalEnemyLogic : AbstractEnemyLogic<Enemy> {

        [SerializeField]
        private Enemy _enemyPrefab;

        [SerializeField]
        private float _spawnTime;

        public bool IsSpawning { get; private set; }

        private List<Enemy> _spawnedEnemies = new List<Enemy>();

        public void SpawnEnemy(Vector3 position) {
            if(IsSpawning) {
                return;
            }
            StartCoroutine(SpawnCoroutine(position));
        }

        private System.Collections.IEnumerator SpawnCoroutine(Vector3 position) {
            IsSpawning = true;
            yield return new WaitForSeconds(_spawnTime);
            var enemy = Instantiate(_enemyPrefab, position, Quaternion.identity, transform.parent);
            var enemyData = Enemy.Data;
            enemy.Init(enemyData.Player, enemyData.CellContainer, enemyData.CollectibleItems);
            _spawnedEnemies.Add(enemy);
            IsSpawning = false;
        }
    }
}

