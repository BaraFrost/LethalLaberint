using UnityEngine;

namespace Game {

    public abstract class AbstractAbility : MonoBehaviour {

        protected PlayerController _player;

        protected virtual bool IsAbilityActive => false;
        public bool CanUseAbility => !IsAbilityActive && !_player.HealthLogic.IsDamaged;
        public abstract void Activate();

        public virtual void Init(PlayerController playerController) {
            _player = playerController;
            _player.HealthLogic.onDamaged += OnPlayerDamaged;
        }

        protected virtual void OnPlayerDamaged() { }
    }
}
