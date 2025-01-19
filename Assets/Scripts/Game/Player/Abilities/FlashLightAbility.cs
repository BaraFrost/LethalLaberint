using UnityEngine;

namespace Game {

    public class FlashLightAbility : AbstractAbility {

        [SerializeField]
        private float _distance;

        [SerializeField]
        private DisableEffect _explosionEffectPrefab;
        private DisableEffect _explosionEffectInstance;

        [SerializeField]
        private LayerMask _layerMask;

        public override void Init(PlayerController playerController) {
            base.Init(playerController);
            _explosionEffectInstance = Instantiate(_explosionEffectPrefab, _player.transform.position, Quaternion.identity, transform);
            _explosionEffectInstance.gameObject.SetActive(false);
        }

        public override void Activate() {
            base.Activate();
            if (_explosionEffectInstance != null) {
                _explosionEffectInstance.transform.position = _player.transform.position;
                _explosionEffectInstance.gameObject.SetActive(true);
            }
            var colliders = Physics.OverlapSphere(_player.transform.position, _distance, _layerMask);
            foreach (var collider in colliders) {
                if (!collider.TryGetComponent<Enemy>(out var enemy) || enemy.VisionLogic == null || enemy.Type == Enemy.EnemyType.Electric) {
                    continue;
                }
                enemy.VisionLogic.TemporarilyDisable(AbilityTime);
            }
        }
    }
}
