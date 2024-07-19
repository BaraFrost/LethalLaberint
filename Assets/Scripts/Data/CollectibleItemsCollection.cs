using Game;
using System.Collections.Generic;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(CollectibleItemsCollection), menuName = "Data/CollectibleItemsCollection")]
    public class CollectibleItemsCollection : ScriptableObject {

        [SerializeField]
        private List<CollectibleItem> _collectibleItems;

        public CollectibleItem GetRandomItem() {
            var randomIndex = Random.Range(0, _collectibleItems.Count);
            return _collectibleItems[randomIndex];
        }
    }
}
