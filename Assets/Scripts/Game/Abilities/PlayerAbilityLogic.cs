using Data;
using System.Linq;
using UnityEngine;

namespace Game {

    public class PlayerAbilityLogic : MonoBehaviour {

        private class SpawnedAbility {
            private int _count;
            private AbstractAbility _ability;

            public SpawnedAbility(AbstractAbility ability, int count) {
                _count = count;
                _ability = ability;
            }

            public bool TryToActivate() {
                if (_count <= 0) {
                    return false;
                }
                _count--;
                _ability.Activate();
                return true;
            }
        }

        [SerializeField]
        private PlayerInputLogic _playerInputLogic;

        private SpawnedAbility[] _abilities;

        public void Init(AbilityInitData[] abilitiesInitData) {
            _abilities = abilitiesInitData.Select(ability => new SpawnedAbility(Instantiate(ability.abilityData.AbstractAbility, gameObject.transform), ability.count)).ToArray();
            _playerInputLogic.onAbilityUsed += ActivateAbility;
        }

        private void ActivateAbility(int index) {
            if(index >= _abilities.Length) {
                return;
            }
            _abilities[index].TryToActivate();
        }
    }
}
