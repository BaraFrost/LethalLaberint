using Data;
using UnityEngine;

namespace Game {

    public class ModifierApplyAbility : AbstractAbility {

        [SerializeField]
        private AbstractModifier _modifier;

        public override bool IsAbilityActive => _player.PlayerModifierLogic.IsModifierActive(_modifier);

        public override void Activate() {
            _player.PlayerModifierLogic.TryToAddModifier(_modifier);
        }
    }
}
