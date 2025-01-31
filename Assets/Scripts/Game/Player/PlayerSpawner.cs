using System;
using System.Collections;
using UnityEngine;

namespace Game {

    public class PlayerSpawner : MonoBehaviour {

        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private PlayerController _playerController;

        [SerializeField]
        private float _respawnTime;

        public void Init(Action playerDeadEvent, GameEntitiesContainer gameEntitiesContainer) {
            _playerController.Init(gameEntitiesContainer);
            gameEntitiesContainer.playerController = _playerController;
            _playerController.HealthLogic.onDamaged += StartRespawnCoroutine;
            _playerController.HealthLogic.onDead += playerDeadEvent;
        }

        public void RevivePlayer() {
            _playerController.HealthLogic.AddHealth();
            _playerController.transform.position = _spawnPoint.transform.position;
            _playerController.HealthLogic.Revive();
        }

        private void StartRespawnCoroutine(Enemy.EnemyType type) {
            if (_playerController.HealthLogic.HealthCount <= 0) {
                return;
            }
            StartCoroutine(RespawnCoroutine());
        }

        private IEnumerator RespawnCoroutine() {
            yield return new WaitForSeconds(_respawnTime);
            _playerController.transform.position = _spawnPoint.transform.position;
            _playerController.HealthLogic.Revive();
        }
    }
}

