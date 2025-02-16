using Data;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = nameof(ModifierStartBonus), menuName = "Data/ModifierStartBonus")]
    public class ModifierStartBonus : AbstractStartBonus {

        [SerializeField]
        private Modifier _modifier;

        public override void Apply(GameEntitiesContainer entitiesContainer) {
            entitiesContainer.playerController.PlayerModifierLogic.TryToAddModifier(_modifier);
        }
    }
}