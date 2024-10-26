using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CollectibleItemsSpawner : MonoBehaviour {

        [SerializeField]
        private CollectibleItemsCollection _itemsCollection;

        [SerializeField]
        private int _itemsCount;

        private List<CollectibleItem> _items = new List<CollectibleItem>();
        public List<CollectibleItem> SpawnedItems => _items;

        public void Spawn(SpawnedLabyrinthCellsContainer cellsContainer, DifficultyProgressionConfig difficultyProgressionConfig) {
            var cells = cellsContainer.AvailableCells;
            var sumMoney = 0;
            var requiredMoney = difficultyProgressionConfig.RequiredMoneyInDay;
            while (sumMoney < requiredMoney) {
                var randomIndex = Random.Range(0, cells.Count);
                var position = cells[randomIndex].gameObject.transform.position;
                cells.RemoveAt(randomIndex);
                var item = _itemsCollection.GetRandomItem();
                sumMoney += item.WalletCollectibleItem.Price;
                var randomAngle = Random.Range(0, 360);
                _items.Add(Instantiate(item, new Vector3(position.x, item.transform.position.y, position.z), Quaternion.AngleAxis(randomAngle, Vector3.up), transform));
            }
        }
    }
}
