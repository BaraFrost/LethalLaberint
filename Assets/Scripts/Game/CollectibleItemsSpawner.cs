using Data;
using UnityEngine;

namespace Game {

    public class CollectibleItemsSpawner : MonoBehaviour {

        [SerializeField]
        private CollectibleItemsCollection _itemsCollection;

        [SerializeField]
        private int _itemsCount;

        public void Spawn(SpawnedLabyrinthCellsContainer cellsContainer) {
            var cells = cellsContainer.AvailableCells;
            for (int i = 0; i < _itemsCount; i++) {
                var randomIndex = Random.Range(0, cells.Count);
                var position = cells[randomIndex].gameObject.transform.position;
                cells.RemoveAt(randomIndex);
                var item = _itemsCollection.GetRandomItem();
                var randomAngle = Random.Range(0, 360);
                Instantiate(item, new Vector3(position.x, item.transform.position.y, position.z), Quaternion.AngleAxis(randomAngle, Vector3.up), transform);
            }
        }
    }
}
