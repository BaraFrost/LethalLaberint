using Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class InventoryAbilityItem : MonoBehaviour {

        [SerializeField]
        private GameObject _selectionBackGround;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private TextMeshProUGUI _textMeshPro;

        [SerializeField]
        private Image _iconImage;

        private int _abilityId;
        public int AbilityId => _abilityId;

        public Action<InventoryAbilityItem> onButtonClicked;

        private int _index;

        public void Init(int abilityId) {
            _abilityId = abilityId;
            _button.onClick.AddListener(() => onButtonClicked?.Invoke(this));
            SetItemData();
        }

        public void SetItemData() {
            var ability = Account.Instance.AbilityDataContainer.GetAbility(_abilityId);
            _textMeshPro.text = Account.Instance.AbilitiesCountData[_abilityId].ToString();
            _iconImage.sprite = ability.Sprite;
        }

        private void Update() {
            _textMeshPro.text = Account.Instance.AbilitiesCountData[_abilityId].ToString();
        }

        public void Select() {
            _selectionBackGround.gameObject.SetActive(true);
        }

        public void Deselect() {
            _selectionBackGround.gameObject.SetActive(false);
        }
    }
}
