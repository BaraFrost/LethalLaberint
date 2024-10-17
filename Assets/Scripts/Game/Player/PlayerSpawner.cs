using Data;
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

        [SerializeField]
        private AbilityInitData[] _initData;

        public void Init() {
            _playerController.HealthLogic.onDamaged += StartRespawnCoroutine;
            _playerController.PlayerAbilityLogic.Init(_initData);
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

