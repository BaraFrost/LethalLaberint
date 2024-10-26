using System.Collections;
using UnityEngine;

namespace Game {

    public class MineVisualLogic : AbstractEnemyLogic<MineEnemy> {

        [SerializeField]
        private DestroyableEffect _effectPrefab;

        [SerializeField]
        private GameObject _mineLamp;

        [SerializeField]
        private float _lampSwitchTime;

        private void Start() {
            if(_mineLamp != null) {
                StartCoroutine(PlayMineLampSwitchEffect());
            }
        }

        private IEnumerator PlayMineLampSwitchEffect() {
            while (true) {
                yield return new WaitForSeconds(_lampSwitchTime);
                _mineLamp.SetActive(!_mineLamp.activeSelf);
            }
        }

        public void PlayExplosionEffect() {
            if (_effectPrefab == null) {
                return;
            }
            Instantiate(_effectPrefab, transform.position, Quaternion.identity, transform.parent);
        }
    }
}

