using Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using YG;

namespace Data {

    [CreateAssetMenu(fileName = nameof(ShopItem), menuName = "Data/ShopItem")]
    public class ShopItem : ScriptableObject {

        public enum Type {
            Chest,
            Boombox,
            FlashLight,
            Blackout,
            Adrenaline,
            Teleport,
            SpeedModifier,
            WalletModifier,
            MoneyModifier,
        }

        [SerializeField]
        private Type _type;
        public Type ItemType => _type;

        [SerializeField]
        private int _price;
        public int Price => _price;

        [SerializeField]
        private AssetReference _spriteAssetReference;
        public AssetReference SpriteAssetReference => _spriteAssetReference;

        [SerializeField]
        private LocalizationText _name;
        public LocalizationText Name => _name;

        [SerializeField]
        private AbstractReward _reward;

        [SerializeField]
        private bool _canBuyByMoney;
        public bool CanBuyByMoney => _canBuyByMoney;

        [SerializeField]
        private bool _canBuyByAdd;
        public bool CanBuyByAdd => _canBuyByAdd;

        public void GiveReward() {
            YG2.MetricaSend("shop_item", "type", _type.ToString());
            _reward.GiveReward();
        }
    }
}
