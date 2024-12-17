using Data;
using System;
using System.Collections.Generic;
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
                if (_count <= 0 || !_ability.CanUseAbility) {
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

        private Dictionary<int, SpawnedAbility> _abilities = new Dictionary<int, SpawnedAbility>();
        public Dictionary<int, SpawnedAbility> Abilities => _abilities;

        private SpawnedAbility _currentAbility;
        public SpawnedAbility CurrentAbility => _currentAbility;

        public Action onAbilityActivate;

        public override void Init(PlayerController player) {
            base.Init(player);
            _playerInputLogic.onAbilityUsed += ActivateAbility;
            Account.Instance.onCurrentAbilityChanged += SwitchAbility;
            SwitchAbility();
        }

        private void SwitchAbility() {
            if (_abilities.ContainsKey(Account.Instance.CurrentAbilityId)) {
                _currentAbility = _abilities[Account.Instance.CurrentAbilityId];
                return;
            }
            var abilityInitData = Account.Instance.CurrentAbilityInitData;
            var spawnedAbility = new SpawnedAbility(Instantiate(abilityInitData.abilityData.AbstractAbility, transform.parent), abilityInitData.count, abilityInitData.abilityData);
            spawnedAbility.Ability.Init(_player);
            _currentAbility = spawnedAbility;
            _abilities.Add(Account.Instance.CurrentAbilityId, spawnedAbility);
        }

        private void ActivateAbility(int index) {
            if (!_abilities.ContainsKey(Account.Instance.CurrentAbilityId)) {
                return;
            }
            if (_abilities[Account.Instance.CurrentAbilityId].TryToActivate()) {
                onAbilityActivate?.Invoke();
            }
        }

        private void OnDestroy() {
            Account.Instance.onCurrentAbilityChanged -= SwitchAbility;
        }
    }
}
