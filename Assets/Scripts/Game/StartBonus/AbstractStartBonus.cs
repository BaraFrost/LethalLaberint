using Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game {

    public abstract class AbstractStartBonus : ScriptableObject {

        [SerializeField]
        private LocalizationText _name;
        public LocalizationText Name => _name;

        [SerializeField]
        private AssetReference _iconAssetReference;
        public AssetReference IconAssetReference => _iconAssetReference;

        public abstract void Apply(GameEntitiesContainer entitiesContainer);
    }
}
