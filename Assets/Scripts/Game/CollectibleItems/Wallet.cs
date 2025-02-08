using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class Wallet : MonoBehaviour {

        [SerializeField]
        private Transform _dropPoint;

        [SerializeField]
        private WalletItemDropPanel _dropPanel;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private GameObject _walletRoot;

        [SerializeField]
        private Animator _bagAnimation;

        private float _startCameraSize;

        private Dictionary<WalletCollectibleItem, CollectibleItem> _items = new Dictionary<WalletCollectibleItem, CollectibleItem>();

        public Action<CollectibleItem> onItemFellOut;

        private void OnEnable() {
            _dropPanel.onItemDropped += OnItemDropped;
            _startCameraSize = _camera.orthographicSize;
        }

        private void OnDisable() {
            _dropPanel.onItemDropped -= OnItemDropped;
        }

        public void OnItemDropped(WalletCollectibleItem walletItem) {
            if (!_items.ContainsKey(walletItem)) {
                return;
            }
            _bagAnimation.Play("BagAnimation");
            var item = _items[walletItem];
            _items.Remove(walletItem);
            Destroy(walletItem.gameObject);
            onItemFellOut?.Invoke(item);
        }

        public void AddItem(CollectibleItem item) {
            _bagAnimation.Play("BagAnimation");
            var randomAngle = UnityEngine.Random.Range(0, 360);
            var spawnedItem = Instantiate(item.WalletCollectibleItem, _dropPoint.transform.position, Quaternion.AngleAxis(randomAngle, Vector3.forward), _dropPoint);
            _items.Add(spawnedItem, item);
        }

        public int GetItemsMoney() {
            var money = 0;
            foreach (var item in _items) {
                money += item.Key.Price;
            }
            return money;
        }

        public List<CollectibleItem> TakeAllItems() {
            var result = new List<CollectibleItem>();
            foreach (var item in _items) {
                result.Add(item.Value);
                Destroy(item.Key.gameObject);
            }
            _items.Clear();
            return result;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
        }

        public void ModifyWalletSize(float multiplier) {
            if (_walletRoot.transform.localScale.x == multiplier) {
                return;
            }
            _walletRoot.transform.localScale = Vector3.one * multiplier;
            _camera.orthographicSize = _startCameraSize * multiplier;
        }
    }
}