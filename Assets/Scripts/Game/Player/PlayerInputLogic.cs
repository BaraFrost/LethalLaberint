using System;
using UnityEngine;

namespace Game {

    public class PlayerInputLogic : AbstractPlayerLogic {

        [SerializeField]
        private VariableJoystick _joystick;

        public Action<int> onAbilityUsed;
        public Action onDoorButtonClicked;

        private Vector3 _inputMoveVector;
        public Vector3 InputMoveVector => _inputMoveVector;

        public override void UpdateLogic() {
            base.UpdateLogic();
            CatchAbilityInput();
            CatchDoorButton();
            CatchMoveInput();
        }

        private void CatchMoveInput() {
            _inputMoveVector = Vector3.zero;
            if (_joystick.Direction.magnitude > 0) {
                _inputMoveVector.x = _joystick.Direction.x;
                _inputMoveVector.z = _joystick.Direction.y;
            } else {
                _inputMoveVector.x = Input.GetAxisRaw("Horizontal");
                _inputMoveVector.z = Input.GetAxisRaw("Vertical");
            }
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

