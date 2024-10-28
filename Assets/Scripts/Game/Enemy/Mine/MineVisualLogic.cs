using System.Collections;
using UnityEngine;

namespace Game {

    public class MineVisualLogic : AbstractVisualLogic<ZoneEnemy> {

        [SerializeField]
        private DestroyableEffect _effectPrefab;

        [SerializeField]
        private GameObject _mineLamp;

        [SerializeField]
        private float _lampSwitchTime;

        public override void Init(Enemy enemy) {
            base.Init(enemy);
            if (_mineLamp != null) {
                StartCoroutine(PlayMineLampSwitchEffect());
            }
        }

        private IEnumerator PlayMineLampSwitchEffect() {
            while (true) {
                yield return new WaitForSeconds(_lampSwitchTime);
                _mineLamp.SetActive(!_mineLamp.activeSelf);
            }
        }

        protected override void PlayAttackVisual() {
            base.PlayAttackVisual();
            if (_effectPrefab == null) {
                return;
            }
            Instantiate(_effectPrefab, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }
}

