using Game;
using Infrastructure;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class StartBonusItem : MonoBehaviour {

        [SerializeField]
        private AsyncImage _asyncImage;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private TextMeshProUGUI _label;

        private AbstractStartBonus _startBonus;
        public AbstractStartBonus StartBonus => _startBonus;

        private Action<AbstractStartBonus> _onClicked;

        private void Awake() {
            _button.onClick.AddListener(GiveBonus);
        }

        public void Init(AbstractStartBonus startBonus, Action<AbstractStartBonus> onClicked) {
            _onClicked = onClicked;
            _startBonus = startBonus;
            _asyncImage.Load(startBonus.IconAssetReference);
            _label.text = startBonus.Name.GetText();
        }

        private void GiveBonus() {
            _onClicked?.Invoke(_startBonus);
        }
    }
}