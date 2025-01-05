using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniGames.FlappyBird {
    public class FlappyBirdObstacleSpawner : MonoBehaviour {

        public FlappyBirdObstacle[] obstaclePrefabs; // Префаб препятствия
        public int poolSize = 5; // Размер пула объектов
        public float spawnRate = 2f; // Частота появления препятствий
        public float minY = -1f; // Минимальная высота
        public float maxY = 3f; // Максимальная высота
        public float spawnXPosition = 10f; // Позиция X для появления препятствий
        public float despawnXPosition = -10f; // Позиция X для удаления препятствий

        private List<FlappyBirdObstacle> obstaclePool; // Список для пула объектов
        private float timer;

        private float _speedTimer;
        [SerializeField]
        private float _maxSpeedTime;
        [SerializeField]
        private float _maxSpeedModifier;

        void Start() {
            InitializePool();
        }

        public void UpdateLogic() {
            timer += Time.deltaTime;
            _speedTimer += Time.deltaTime;
            if (timer >= spawnRate / Mathf.Lerp(1, _maxSpeedModifier, Mathf.Min(_speedTimer / _maxSpeedTime, 1))) {
                SpawnObstacle();
                timer = 0;
            }
            UpdateObstacles();
        }

        private void UpdateObstacles() {
            foreach (var obstacle in obstaclePool) {
                if (obstacle.gameObject.activeInHierarchy && obstacle.transform.position.x < despawnXPosition) {
                    obstacle.gameObject.SetActive(false);
                    continue;
                }
                if (obstacle.gameObject.activeInHierarchy) {
                    obstacle.UpdateObstacle(Mathf.Lerp(1, _maxSpeedModifier, Mathf.Min(_speedTimer / _maxSpeedTime, 1)));
                }
            }
        }

        // Инициализация пула объектов
        private void InitializePool() {
            obstaclePool = new List<FlappyBirdObstacle>();

            foreach (var obstacle in obstaclePrefabs) {
                for (int i = 0; i < poolSize; i++) {
                    FlappyBirdObstacle obj = Instantiate(obstacle);
                    obj.gameObject.SetActive(false);
                    obstaclePool.Add(obj);
                }
            }
        }

        // Спавн препятствия из пула
        private void SpawnObstacle() {
            FlappyBirdObstacle obstacle = GetInactiveObstacle();

            if (obstacle != null) {
                float randomY = Random.Range(minY, maxY);
                obstacle.transform.position = new Vector3(spawnXPosition, randomY, 0);
                obstacle.gameObject.SetActive(true);
            }
        }

        // Возвращает неактивный объект из пула
        private FlappyBirdObstacle GetInactiveObstacle() {
            var inactiveObstacle = obstaclePool.Where(obstacle => !obstacle.gameObject.activeInHierarchy).ToArray();
            if (inactiveObstacle.Length == 0) {
                return null;
            }
            return inactiveObstacle[Random.Range(0, inactiveObstacle.Length)];
        }

    }
}
