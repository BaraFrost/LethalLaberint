using Data;
using UnityEngine;

namespace Game {

    public class ModifierApplyAbility : AbstractAbility {

        [SerializeField]
        private Modifier _modifier;

        public override float AbilityTime => _modifier.ModifierTime;

        public override void Activate() {
            base.Activate();
            _player.PlayerModifierLogic.TryToAddModifier(_modifier);
        }

        protected override void OnPlayerDamaged(Enemy.EnemyType enemyType) {
            base.OnPlayerDamaged(enemyType);
            _player.PlayerModifierLogic.RemoveModifier(_modifier);
        }
    }
}
