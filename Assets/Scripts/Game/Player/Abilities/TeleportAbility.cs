using System.Collections;
using UnityEngine;

namespace Game {

    public class TeleportAbility : AbstractAbility {

        [SerializeField]
        private float _teleportTime;
        [SerializeField]
        private ParticleSystem _teleportEffect;
        [SerializeField]
        private float _additionalEffectTime;
        private bool _isTeleporting;
        private Vector3 _startPosition;

        public override void Init(PlayerController playerController) {
            base.Init(playerController);
            _startPosition = _player.transform.position;
        }

        public override void Activate() {
            if (_isTeleporting) {
                return;
            }
            StartCoroutine(TeleportCoroutine());
        }

        private IEnumerator TeleportCoroutine() {
            _isTeleporting = true;
            _teleportEffect.gameObject.SetActive(true);
            yield return new WaitForSeconds(_teleportTime);
            _player.PlayerMoveLogic.Teleport(_startPosition);
            yield return new WaitForSeconds(_additionalEffectTime);
            _teleportEffect.gameObject.SetActive(false);
            _isTeleporting = false;
        }

        private void OnEnable() {
            _teleportEffect.gameObject.SetActive(false);
        }

        private void OnDisable() {
            _teleportEffect.gameObject.SetActive(false);
        }
    }
}
