using Data;
using System;
using System.Linq;
using UnityEngine;

namespace Game {

    public class PlayerAbilityLogic : MonoBehaviour {

        public class SpawnedAbility {

            private int _count;
            public int Count => _count;

            private AbstractAbility _ability;
            public AbstractAbility Ability => _ability;

            public AbilityData AbilityData { get; private set; }

            public SpawnedAbility(AbstractAbility ability, int count, AbilityData abilityData) {
                AbilityData = abilityData;
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
        public SpawnedAbility[] Abilities => _abilities;

        public Action onAbilityActivate;

        public void Init(AbilityInitData[] abilitiesInitData) {
            _abilities = abilitiesInitData.Select(ability =>
                new SpawnedAbility(Instantiate(ability.abilityData.AbstractAbility, gameObject.transform), ability.count, ability.abilityData)).ToArray();
            _playerInputLogic.onAbilityUsed += ActivateAbility;
        }

        private void ActivateAbility(int index) {
            if (index >= _abilities.Length) {
                return;
            }
            if (_abilities[index].TryToActivate()) {
                onAbilityActivate?.Invoke();
            }
        }
    }
}
