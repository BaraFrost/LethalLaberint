using System;
using UnityEngine;

namespace Game {

    public class PlayerInputLogic : MonoBehaviour {

        public Action<int> onAbilityUsed;
        public Action onDoorButtonClicked;

        private void Update() {
            CatchAbilityInput();
            CatchDoorButton();
        }

        private void CatchDoorButton() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                onDoorButtonClicked?.Invoke();
            }
        }

        private void CatchAbilityInput() {
            if(Input.GetKeyDown(KeyCode.F)) {
                onAbilityUsed?.Invoke(0);
            }
        }

        public void AbilityButtonActivate(int index) {
            onAbilityUsed?.Invoke(index);
        }
    }
}

