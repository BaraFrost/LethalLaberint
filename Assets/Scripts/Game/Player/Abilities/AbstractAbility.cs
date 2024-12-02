using UnityEngine;

namespace Game {

    public abstract class AbstractAbility : MonoBehaviour {

        protected PlayerController _player;

        public virtual bool IsAbilityActive => false;
        public abstract void Activate();

        public virtual void Init(PlayerController playerController) {
            _player = playerController;
        }
    }
}
