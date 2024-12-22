using Data;
using UnityEngine;

namespace Game {

    public class ModifierApplyAbility : AbstractAbility {

        [SerializeField]
        private Modifier _modifier;

        protected override bool IsAbilityActive => _player.PlayerModifierLogic.IsModifierActive(_modifier);

        public override void Activate() {
            _player.PlayerModifierLogic.TryToAddModifier(_modifier);
        }

        protected override void OnPlayerDamaged() {
            base.OnPlayerDamaged();
            _player.PlayerModifierLogic.RemoveModifier(_modifier);
        }
    }
}
