using Game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI {

    public class ItemsCountVisualizer : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _text;

        private List<CollectibleItem> _collectibleItems;

        public void Init(List<CollectibleItem> collectibleItems) {
            _collectibleItems = collectibleItems;
        }

        public void Update() {
            var collectedItemsCount = 0;
            for (var i = 0; i < _collectibleItems.Count; i++) {
                if (_collectibleItems[i].Collected) {
                    collectedItemsCount++;
                }
            }
            _text.text = $"{collectedItemsCount}/{_collectibleItems.Count}";
        }
    }
}

