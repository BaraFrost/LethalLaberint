using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class SpawnAdditionalEnemyLogic : AbstractEnemyLogic<Enemy> {

        [SerializeField]
        private Enemy _enemyPrefab;

        [SerializeField]
        private float _spawnTime;

        [SerializeField]
        private bool _useOffset;
        [SerializeField]
        private float _offset;

        public bool IsSpawning { get; private set; }

        private List<Enemy> _spawnedEnemies = new List<Enemy>();

        public Action onEnemySpawned;
        private Coroutine _spawnCoroutine;

        public void SpawnEnemy(Vector3 position) {
            if (IsSpawning) {
                return;
            }
            _spawnCoroutine = StartCoroutine(SpawnCoroutine(position));
        }

        public void StopSpawnEnemy() {
            if (!IsSpawning || _spawnCoroutine == null) {
                return;
            }
            IsSpawning = false;
            StopCoroutine(_spawnCoroutine);
        }

        private System.Collections.IEnumerator SpawnCoroutine(Vector3 position) {
            IsSpawning = true;
            yield return new WaitForSeconds(_spawnTime);
            var offset = Vector3.zero;
            if (_useOffset) {
                offset = new Vector3(UnityEngine.Random.Range(-_offset, _offset), 0, UnityEngine.Random.Range(-_offset, _offset));
            }
            var enemy = Instantiate(_enemyPrefab, position + offset, Quaternion.identity, transform.parent);
            var enemyData = Enemy.Data;
            enemy.Init(enemyData.Player, enemyData.CellContainer, enemyData.CollectibleItems);
            _spawnedEnemies.Add(enemy);
            IsSpawning = false;
            onEnemySpawned?.Invoke();
        }

        public bool IsChildEnemyAttack(out Enemy attackingEnemy) {
            attackingEnemy = null;
            foreach (var enemy in _spawnedEnemies) {
                if (enemy.AttackLogic != null && enemy.AttackLogic.IsAttacking) {
                    attackingEnemy = enemy;
                    return true;
                }
            }
            return false;
        }
    }
}

