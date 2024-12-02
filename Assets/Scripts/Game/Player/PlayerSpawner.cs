using System;
using System.Collections;
using UnityEngine;

namespace Game {

    public class PlayerSpawner : MonoBehaviour {

        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private PlayerController _playerController;
        public PlayerController PlayerController => _playerController;

        [SerializeField]
        private float _respawnTime;

        public void Init(Action playerDeadEvent) {
            _playerController.Init();
            _playerController.HealthLogic.onDamaged += StartRespawnCoroutine;
            _playerController.HealthLogic.onDead += playerDeadEvent;
        }

        private void StartRespawnCoroutine() {
            if(_playerController.HealthLogic.HealthCount <= 0) {
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

