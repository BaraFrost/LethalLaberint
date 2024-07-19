using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class Wallet : MonoBehaviour {

        [SerializeField]
        private Transform _dropPoint;

        [SerializeField]
        private WalletItemDropPanel _dropPanel;

        private Dictionary<WalletCollectibleItem, CollectibleItem> _items = new Dictionary<WalletCollectibleItem, CollectibleItem>();

        public Action<CollectibleItem> onItemFellOut;

        private void OnEnable() {
            _dropPanel.onItemDropped += OnItemDropped;
        }

        private void OnDisable() {
            _dropPanel.onItemDropped -= OnItemDropped;
        }

        public void OnItemDropped(WalletCollectibleItem walletItem) {
            var item = _items[walletItem];
            _items.Remove(walletItem);
            Destroy(walletItem.gameObject);
            onItemFellOut?.Invoke(item);
        }

        public void AddItem(CollectibleItem item) {
            var randomAngle = UnityEngine.Random.Range(0, 360);
            var spawnedItem = Instantiate(item.WalletCollectibleItem, _dropPoint.transform.position, Quaternion.AngleAxis(randomAngle, Vector3.forward), _dropPoint);
            _items.Add(spawnedItem, item);
        }

        public int GetItemsMoneyAndDestroyItems() {
            var money = _items.Count;
            foreach (var item in _items) {
                Destroy(item.Key.gameObject);
            }
            _items.Clear();
            return money;
        }
    }
}
