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

        public void Start() {
            _button.onClick.AddListener(TryToBuyItem);
            UpdateMoneyText();
        }

        protected virtual void UpdateMoneyText() {
            _text.text = $"{Account.Instance.GetShopItemPrice(_shopItemType).ToString()}$";
            _image.sprite = Account.Instance.GetShopItemSprite(_shopItemType);
            _nameTextLabel.text = Account.Instance.GetShopItemName(_shopItemType).GetText();
        }

        public void TryToBuyItem() {
            Account.Instance.TryToByItem(_shopItemType);
            UpdateMoneyText();
        }
    }
}
