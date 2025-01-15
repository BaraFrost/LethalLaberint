using Data;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class BookItem : MonoBehaviour {

        [SerializeField]
        private Image _image;

        [SerializeField]
        private TextMeshProUGUI _textLabel;

        [SerializeField]
        private TextMeshProUGUI _titleTextLabel;

        [SerializeField]
        private Sprite _closedSprite;

        [SerializeField]
        private LocalizationText _closedText;

        private BookItemsData.Data _data;

        public void SetData(BookItemsData.Data data) {
            _data = data;
            if (!Account.Instance.OpenedEnemies[data.enemyType]) {
                _image.sprite = _closedSprite;
                _textLabel.text = _closedText.GetText();
                return;
            }
            _image.sprite = _data.enemyImage;
            _textLabel.text = _data.description.GetText();
            _titleTextLabel.text = _data.title.GetText();
        }
    }
}

