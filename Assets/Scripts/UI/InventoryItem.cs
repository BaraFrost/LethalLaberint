using Game;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class InventoryItem : MonoBehaviour {

        [SerializeField]
        private Button _button;

        [SerializeField]
        private TextMeshProUGUI _countText;

        [SerializeField]
        private Image _iconImage;

        [SerializeField]
        private Image _progressImage;

        public Action<int> onButtonClicked;

        private int _index;

        public void Init(int index, PlayerAbilityLogic.SpawnedAbility ability) {
            _index = index;
            _button.onClick.AddListener(() => onButtonClicked?.Invoke(_index));
            UpdateItem(ability);
        }

        public void UpdateItem(PlayerAbilityLogic.SpawnedAbility ability) {
            _countText.text = ability.Count.ToString();
            _iconImage.sprite = ability.AbilityData.Sprite;
        }
    }
}

