using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(AbilityReward), menuName = "Data/AbilityReward")]
    public class AbilityReward : AbstractReward {
        [SerializeField]
        private int _abilityId;
        
        public override void GiveReward() {
            Account.Instance.AddAbility(_abilityId);
        }
    }
}
