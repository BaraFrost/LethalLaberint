using UnityEngine;

namespace Game {

    public class FlashLightAbility : AbstractAbility {

        [SerializeField]
        private float _distance;

        [SerializeField]
        private float _flashTime;

        [SerializeField]
        private DestroyableEffect _explosionEffect;

        [SerializeField]
        private LayerMask _layerMask;

        public override void Activate() {
            if (_explosionEffect != null) {
                Instantiate(_explosionEffect, _player.transform.position, Quaternion.identity, transform);
            }
            var colliders = Physics.OverlapSphere(_player.transform.position, _distance, _layerMask);
            foreach (var collider in colliders) {
                if (!collider.TryGetComponent<Enemy>(out var enemy) || enemy.VisionLogic == null) {
                    continue;
                }
                enemy.VisionLogic.TemporarilyDisable(_flashTime);
            }
        }
    }
}
