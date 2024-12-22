using Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game {

    public class PlayerModifierLogic : AbstractPlayerLogic {

        private class TemporaryModifier {
            public Modifier modifier;
            public float remainingTime;
        }

        private List<Modifier> _constantModifiers = new List<Modifier>();
        private List<TemporaryModifier> _temporaryModifiers = new List<TemporaryModifier>();

        private HashSet<Modifier> _activeModifiers = new HashSet<Modifier>();

        public float SpeedModifier { get; private set; } = 1f;
        public float WalletModifier { get; private set; } = 1f;
        public float MoneyModifier { get; private set; } = 1f;

        public bool IsModifierActive(Modifier modifier) => _activeModifiers.Contains(modifier);

        public override void Init(PlayerController player) {
            base.Init(player);
            var levelModifiers = Account.Instance.LevelsModifiersContainer.Modifiers;
            foreach (var levelModifier in levelModifiers) {
                TryToAddModifier(levelModifier.Value);
            }
        }

        public bool TryToAddModifier(Modifier modifier) {
            if (IsModifierActive(modifier)) {
                return false;
            }
            if (modifier.Temporary) {
                _temporaryModifiers.Add(new TemporaryModifier() {
                    modifier = modifier,
                    remainingTime = modifier.ModifierTime,
                });
            } else {
                _constantModifiers.Add(modifier);
            }
            _activeModifiers.Add(modifier);
            return true;
        }

        private void ResetModifierValues() {
            SpeedModifier = 1f;
            WalletModifier = 1f;
            MoneyModifier = 1f;
        }

        public override void UpdateLogic() {
            base.UpdateLogic();
            ResetModifierValues();
            foreach (var modifier in _constantModifiers) {
                ApplyModifier(modifier);
            }
            for (var i = _temporaryModifiers.Count - 1; i >= 0; i--) {
                _temporaryModifiers[i].remainingTime -= Time.deltaTime;
                if (_temporaryModifiers[i].remainingTime <= 0) {
                    _activeModifiers.Remove(_temporaryModifiers[i].modifier);
                    _temporaryModifiers.RemoveAt(i);
                    continue;
                }
                ApplyModifier(_temporaryModifiers[i].modifier);
            }
        }

        public void RemoveModifier(Modifier modifier) {
            if (!IsModifierActive(modifier)) {
                return;
            }
            _activeModifiers.Remove(modifier);
            if (modifier.Temporary) {
                var temporaryModifier = _temporaryModifiers.First(temporaryModifier => temporaryModifier.modifier == modifier);
                _temporaryModifiers.Remove(temporaryModifier);
            } else {
                _constantModifiers.Remove(modifier);
            }
        }

        private void ApplyModifier(Modifier modifier) {
            switch (modifier.Type) {
                case ModifierType.Speed:
                    SpeedModifier += modifier.Value;
                    break;
                case ModifierType.Wallet:
                    WalletModifier += modifier.Value;
                    break;
                case ModifierType.Money:
                    MoneyModifier += modifier.Value;
                    break;
            }
        }

        public bool ShowModifierEffect() {
            foreach (var modifier in _temporaryModifiers) {
                if (modifier.modifier.ShowModifierEffect) {
                    return true;
                }
            }
            return false;
        }
    }
}