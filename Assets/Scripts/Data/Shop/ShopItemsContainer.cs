using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(ShopItemsContainer), menuName = "Data/ShopItemsContainer")]
    public class ShopItemsContainer : ScriptableObject {

        [SerializeField]
        private ShopItem[] _shopItems;

        private Dictionary<ShopItem.Type, ShopItem> _cashedShopItems;
        private Dictionary<ShopItem.Type, ShopItem> CashedShopItems {
            get {
                if (_cashedShopItems == null) {
                    _cashedShopItems = _shopItems.ToDictionary(item => item.ItemType, item => item);
                }
                return _cashedShopItems;
            }
        }

        public ShopItem GetShopItemByType(ShopItem.Type type) {
            return CashedShopItems[type];
        }
    }
}