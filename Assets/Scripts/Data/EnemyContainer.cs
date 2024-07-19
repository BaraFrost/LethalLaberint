using Game;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(EnemyContainer), menuName = "Data/EnemyContainer")]
    public class EnemyContainer : ScriptableObject {

        [SerializeField]
        private Enemy[] _enemies;
        public Enemy[] Enemies => _enemies;

        public Enemy GetRandomEnemy() {
            var randomIndex = Random.Range(0, _enemies.Length);
            return _enemies[randomIndex];
        }
    }
}
