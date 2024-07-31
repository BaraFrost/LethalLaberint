using UnityEngine;

namespace Game {

    public class PlayerAbilityLogic : MonoBehaviour {

        [SerializeField]
        private AbstractAbility[] _abilities;

        private void Update() {
            if(Input.GetKeyDown(KeyCode.F)) {
                _abilities[0].Activate();
            }
        }
    }
}
