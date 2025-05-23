using UnityEngine;

namespace Game {

    public class TeleportAbility : AbstractAbility {

        [SerializeField]
        private ParticleSystem _teleportEffectPrefab;
        private ParticleSystem _teleportEffect;
        [SerializeField]
        private float _additionalEffectTime;
        private Vector3 _startPosition;

        public override void Init(PlayerController playerController) {
            base.Init(playerController);
            _startPosition = _player.transform.position;
            _teleportEffect = Instantiate(_teleportEffectPrefab, _player.transform.position, Quaternion.identity, _player.transform);
        }

        public override void Activate() {
            base.Activate();
            _teleportEffect.Play(withChildren: true);
        }

        protected override void Stop() {
            base.Stop();
            _player.PlayerMoveLogic.Teleport(_startPosition);
            _player.PlayerMoveLogic.FreezeMovement(_additionalEffectTime);
            _teleportEffect.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
        }

        protected override void OnPlayerDamaged(Enemy.EnemyType enemyType) {
            base.OnPlayerDamaged(enemyType);
            _teleportEffect.Stop();
        }
    }
}
