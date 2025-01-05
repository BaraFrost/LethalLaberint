using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniGames.FlappyBird {
    public class FlappyBirdObstacleSpawner : MonoBehaviour {

        public FlappyBirdObstacle[] obstaclePrefabs; // ������ �����������
        public int poolSize = 5; // ������ ���� ��������
        public float spawnRate = 2f; // ������� ��������� �����������
        public float minY = -1f; // ����������� ������
        public float maxY = 3f; // ������������ ������
        public float spawnXPosition = 10f; // ������� X ��� ��������� �����������
        public float despawnXPosition = -10f; // ������� X ��� �������� �����������

        private List<FlappyBirdObstacle> obstaclePool; // ������ ��� ���� ��������
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

        // ������������� ���� ��������
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

        // ����� ����������� �� ����
        private void SpawnObstacle() {
            FlappyBirdObstacle obstacle = GetInactiveObstacle();

            if (obstacle != null) {
                float randomY = Random.Range(minY, maxY);
                obstacle.transform.position = new Vector3(spawnXPosition, randomY, 0);
                obstacle.gameObject.SetActive(true);
            }
        }

        // ���������� ���������� ������ �� ����
        private FlappyBirdObstacle GetInactiveObstacle() {
            var inactiveObstacle = obstaclePool.Where(obstacle => !obstacle.gameObject.activeInHierarchy).ToArray();
            if (inactiveObstacle.Length == 0) {
                return null;
            }
            return inactiveObstacle[Random.Range(0, inactiveObstacle.Length)];
        }

    }
}
