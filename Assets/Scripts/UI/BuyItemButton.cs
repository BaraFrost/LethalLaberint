using Data;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class BuyItemButton : MonoBehaviour {

        [SerializeField]
        private ShopItem.Type _shopItemType;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private GameObject _priceGroup;

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private AsyncImage _asyncImage;

        [SerializeField]
        private TextMeshProUGUI _nameTextLabel;

        [SerializeField]
        private GameObject _cantBuyGroup;

        [SerializeField]
        private GameObject _addImage;

        public void Start() {
            var shopItem = Account.Instance.GetShopItemByType(_shopItemType);
            _button.onClick.AddListener(TryToBuyItem);
            _asyncImage.Load(shopItem.SpriteAssetReference);
        }

        protected virtual void UpdateMoneyText() {
            var shopItem = Account.Instance.GetShopItemByType(_shopItemType);
            UpdateBuyGroup(shopItem);
            if(_text != null) {
                _text.text = $"{shopItem.Price.ToString()}$";
            }
            _nameTextLabel.text = shopItem.Name.GetText();
        }

        private void UpdateBuyGroup(ShopItem shopItem) {
            var canBuyByMoney = Account.Instance.TotalMoney >= shopItem.Price && shopItem.CanBuyByMoney;
            if(_priceGroup != null) {
                _priceGroup.SetActive(canBuyByMoney);
            }
            _cantBuyGroup.SetActive(!canBuyByMoney);
            _addImage.SetActive(!canBuyByMoney && shopItem.CanBuyByAdd);
        }

        private void Update() {
            UpdateMoneyText();
        }

        public void TryToBuyItem() {
            Account.Instance.TryToByItem(_shopItemType);
        }
    }
}
