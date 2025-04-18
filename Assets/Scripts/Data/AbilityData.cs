using Game;
using Infrastructure;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data {

    [CreateAssetMenu(fileName = nameof(AbilityData), menuName = "Data/AbilityData")]
    public class AbilityData : ScriptableObject {

        [ReadOnly]
        public int id = -1;

        [SerializeField]
        private AbstractAbility _abstractAbility;
        public AbstractAbility AbstractAbility => _abstractAbility;

        [SerializeField]
        private AssetReference _spriteAssetReference;
        public AssetReference SpriteAssetReference => _spriteAssetReference;

        [SerializeField]
        private LocalizationText _abilityName;
        public LocalizationText AbilityName => _abilityName;

        [SerializeField]
        private LocalizationText _abilityDescription;
        public LocalizationText AbilityDescription => _abilityDescription;

#if UNITY_EDITOR
        [Button]
        private void ResetId() {
            id = -1;
        }
#endif
    }
}
