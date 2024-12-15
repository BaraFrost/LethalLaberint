using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class PlayerModifierLogic : AbstractPlayerLogic {

        private class TemporaryModifier {
            public AbstractModifier modifier;
            public float remainingTime;
        }

        private List<AbstractModifier> _constantModifiers = new List<AbstractModifier>();
        private List<TemporaryModifier> _temporaryModifiers = new List<TemporaryModifier>();

        private HashSet<AbstractModifier> _activeModifiers = new HashSet<AbstractModifier>();

        public float SpeedModifier { get; private set; } = 1f;

        public bool IsModifierActive(AbstractModifier modifier) => _activeModifiers.Contains(modifier);

        public bool TryToAddModifier(AbstractModifier modifier) {
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

        private void ApplyModifier(AbstractModifier modifier) {
            switch (modifier.Type) {
                case ModifierType.Speed:
                    SpeedModifier += modifier.Value;
                    break;
            }
        }

        public bool ShowModifierEffect() {
            foreach (var modifier in _temporaryModifiers) {
                if(modifier.modifier.ShowModifierEffect) {
                    return true;
                }
            }
            return false;
        }
    }
}