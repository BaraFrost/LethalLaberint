using Data;
using System;
using UnityEngine;

namespace Game {

    public class PlayerAbilityLogic : AbstractPlayerLogic {

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
                if (_count <= 0 || _ability.IsAbilityActive) {
                    return false;
                }
                _count--;
                _ability.Activate();
                Account.Instance.OnAbilityUsed(AbilityData.id);
                return true;
            }
        }

        [SerializeField]
        private PlayerInputLogic _playerInputLogic;

        private SpawnedAbility[] _abilities;
        public SpawnedAbility[] Abilities => _abilities;

        public Action onAbilityActivate;

        public override void Init(PlayerController player) {
            base.Init(player);
            var abilityInitData = Account.Instance.CurrentAbilityInitData;
            var spawnedAbility = new SpawnedAbility(Instantiate(abilityInitData.abilityData.AbstractAbility, gameObject.transform), abilityInitData.count, abilityInitData.abilityData);
            spawnedAbility.Ability.Init(player);
            _abilities = new SpawnedAbility[] { spawnedAbility };
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
