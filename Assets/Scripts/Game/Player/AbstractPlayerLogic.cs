using UnityEngine;

namespace Game {

    public class AbstractPlayerLogic : MonoBehaviour {

        protected PlayerController _player;

        public virtual void Init(PlayerController player) {
            _player = player;
        }

        public virtual void UpdateLogic() { }
    }
}

