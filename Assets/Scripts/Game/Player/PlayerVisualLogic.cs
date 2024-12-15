using UnityEngine;

namespace Game {

    [RequireComponent(typeof(Animator))]
    public class PlayerVisualLogic : AbstractPlayerLogic {

        [SerializeField]
        private ParticleSystem _modifierEffect;
        [SerializeField]
        private GameEnvironmentVisualLogic _environmentVisualLogic;
        public GameEnvironmentVisualLogic EnvironmentVisualLogic => _environmentVisualLogic;

        private Animator _animator;

        public override void Init(PlayerController player) {
            base.Init(player);
            _animator = GetComponent<Animator>();
            player.PlayerMoveLogic.onStartMove += StartMoveAnimation;
            player.PlayerMoveLogic.onStopMove += StopMoveAnimation;
        }

        public override void UpdateLogic() {
            base.UpdateLogic();
            var showModifierEffect = _player.PlayerModifierLogic.ShowModifierEffect();
            if (showModifierEffect && !_modifierEffect.isPlaying) {
                _modifierEffect.Play();
            } else if (!showModifierEffect && _modifierEffect.isPlaying) {
                _modifierEffect.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
            }

        }

        private void StartMoveAnimation() {
            _animator.SetTrigger("Move");
            _animator.ResetTrigger("Stop");
        }

        private void StopMoveAnimation() {
            _animator.ResetTrigger("Move");
            _animator.SetTrigger("Stop");
        }
    }
}
