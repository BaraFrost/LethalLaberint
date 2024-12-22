using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(ModifierReward), menuName = "Data/ModifierReward")]
    public class ModifierReward : AbstractReward {

        [SerializeField]
        private ModifierType _modifierType;

        public override void GiveReward() {
            if (!Account.Instance.ModifiersCountData.ContainsKey(_modifierType)) {
                return;
            }
            Account.Instance.ModifiersCountData[_modifierType]++;
        }
    }
}
