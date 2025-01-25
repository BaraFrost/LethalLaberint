using Data;
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
        private TextMeshProUGUI _text;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private TextMeshProUGUI _nameTextLabel;

        [SerializeField]
        private GameObject _cantBuyGroup;

        [SerializeField]
        private GameObject _addImage;

        public void Start() {
            _button.onClick.AddListener(TryToBuyItem);
        }

        protected virtual void UpdateMoneyText() {
            var shopItem = Account.Instance.GetShopItemByType(_shopItemType);
            UpdateBuyGroup(shopItem);
            if(_text != null) {
                _text.text = $"{shopItem.Price.ToString()}$";
            }
            _image.sprite = shopItem.Sprite;
            _nameTextLabel.text = shopItem.Name.GetText();
        }

        private void UpdateBuyGroup(ShopItem shopItem) {
            var canBuyByMoney = Account.Instance.TotalMoney >= shopItem.Price && shopItem.CanBuyByMoney;
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
