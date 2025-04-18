using Game;
using Infrastructure;
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
        private AsyncImage _asyncIconImage;

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
            _asyncIconImage.Load(ability.AbilityData.SpriteAssetReference);
        }

        public void UpdateProgress(PlayerAbilityLogic.SpawnedAbility spawnedAbility) {
            _progressImage.gameObject.SetActive(spawnedAbility.Ability.IsAbilityActive);
            if (spawnedAbility.Ability.IsAbilityActive) {
                _progressImage.fillAmount = 1 - spawnedAbility.Ability.Progress;
            }
        }
    }
}