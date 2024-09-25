using System;
using UnityEngine;

namespace Game {

    public class PlayerInputLogic : MonoBehaviour {

        public Action<int> onAbilityUsed;

        private void Update() {
            CatchAbilityInput();
        }

        private void CatchAbilityInput() {
            if(Input.GetKeyDown(KeyCode.Alpha1)) {
                onAbilityUsed?.Invoke(0);
            }
        }
    }
}

