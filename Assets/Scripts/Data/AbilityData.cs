using Game;
using NaughtyAttributes;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(AbilityData), menuName = "Data/AbilityData")]
    public class AbilityData : ScriptableObject {

        [ReadOnly]
        public int id = -1;

        [SerializeField]
        private AbstractAbility _abstractAbility;
        public AbstractAbility AbstractAbility => _abstractAbility;

        [SerializeField]
        private Sprite _sprite;
        public Sprite Sprite => _sprite;

#if UNITY_EDITOR
        [Button]
        private void ResetId() {
            id = -1;
        }
#endif
    }
}
