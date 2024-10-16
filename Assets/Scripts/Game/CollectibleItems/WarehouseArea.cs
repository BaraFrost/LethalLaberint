using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class WarehouseArea : MonoBehaviour {

        [SerializeField]
        private float _randomOffset;

        public void AddItems(List<CollectibleItem> collectibleItems) {
            foreach (var item in collectibleItems) {
                item.transform.position = new Vector3(transform.position.x + Random.Range(0, _randomOffset), item.transform.position.y, transform.position.z + Random.Range(0, _randomOffset));
                item.DisableCollectibleItem();
                item.gameObject.SetActive(true);
            }
        }
    }
}

