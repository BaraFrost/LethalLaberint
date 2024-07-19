using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EnemyContainer _enemyContainer;

        private List<Enemy> _enemies = new List<Enemy>();

        [SerializeField]
        private int _enemyCount;

        public List<Enemy> Spawn(PlayerController player, SpawnedLabyrinthCellsContainer labyrinthCells) {
            for (var i = 0; i < _enemyCount; i++) {
                _enemies.Add(SpawnRandomEnemy(player, labyrinthCells));
            }
            return _enemies;
        }

        private Enemy SpawnRandomEnemy(PlayerController player, SpawnedLabyrinthCellsContainer labyrinthCells) {
            var enemyToSpawn = _enemyContainer.GetRandomEnemy();
            var positionToSpawn = labyrinthCells.GetRandomCellPosition();
            var enemy = Instantiate(enemyToSpawn, positionToSpawn, Quaternion.identity, transform);
            enemy.Init(player, labyrinthCells);
            return enemy;
        }
    }
}

