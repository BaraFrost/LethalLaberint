using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = nameof(HealthStartBonus), menuName = "Data/HealthStartBonus")]
    public class HealthStartBonus : AbstractStartBonus {

        [SerializeField]
        private int _additionalHealth;

        public override void Apply(GameEntitiesContainer entitiesContainer) {
            entitiesContainer.playerController.HealthLogic.AddHealth(_additionalHealth);
        }
    }
}
