using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(ChestReward), menuName = "Data/ChestReward")]
    public class ChestReward : AbstractReward {

        [SerializeField]
        private int _abilitiesCount;

        public override void GiveReward() {
            var allAbilities = Account.Instance.AbilityDataContainer.GetAllAbilityIds();
            for (var i = 0; i < _abilitiesCount; i++) {
                var randomId = allAbilities[Random.Range(0, allAbilities.Length)];
                Account.Instance.AddAbility(randomId);
            }
        }
    }
}