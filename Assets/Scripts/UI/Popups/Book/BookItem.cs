using Data;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI {

    public class BookItem : MonoBehaviour {

        [SerializeField]
        private AsyncImage _asyncImage;

        [SerializeField]
        private TextMeshProUGUI _textLabel;

        [SerializeField]
        private TextMeshProUGUI _titleTextLabel;

        [SerializeField]
        private AssetReference _closedSpriteReference;

        [SerializeField]
        private LocalizationText _closedText;

        private BookItemsData.Data _data;

        public void SetData(BookItemsData.Data data) {
            _data = data;
            if (!Account.Instance.OpenedEnemies[data.enemyType]) {
                _asyncImage.Load(_closedSpriteReference);
                _textLabel.text = _closedText.GetText();
                _titleTextLabel.text = _closedText.GetText();
                return;
            }
            _asyncImage.Load(_data.enemyImageAssetReference);
            _textLabel.text = _data.description.GetText();
            _titleTextLabel.text = _data.title.GetText();
        }
    }
}

