using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class ItemsCollector : AbstractPlayerLogic {

        [SerializeField]
        private Wallet _wallet;
        public Wallet Wallet => _wallet;

        private List<CollectibleItem> _feltCollectibleItems = new List<CollectibleItem>();

        private void OnEnable() {
            _wallet.onItemFellOut += DropItemCollectibleItem;
        }

        private void OnDisable() {
            _wallet.onItemFellOut -= DropItemCollectibleItem;
        }

        public override void UpdateLogic() {
            base.UpdateLogic();
            Wallet.ModifyWalletSize(_player.PlayerModifierLogic.WalletModifier);
        }

        public void DropAllItems() {
            var items = _wallet.TakeAllItems();
            foreach (var item in items) {
                DropItemCollectibleItem(item, addToIgnoreColliderEnter: false);
            }
        }

        private void DropItemCollectibleItem(CollectibleItem collectibleItem) {
            DropItemCollectibleItem(collectibleItem, addToIgnoreColliderEnter: true);
        }

        private void DropItemCollectibleItem(CollectibleItem collectibleItem, bool addToIgnoreColliderEnter) {
            collectibleItem.transform.position = new Vector3(gameObject.transform.position.x, collectibleItem.transform.position.y, gameObject.transform.position.z);
            collectibleItem.gameObject.SetActive(true);
            collectibleItem.Drop();
            if (addToIgnoreColliderEnter) {
                _feltCollectibleItems.Add(collectibleItem);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<CollectibleItem>(out var item)) {
                if (_feltCollectibleItems.Contains(item)) {
                    _feltCollectibleItems.Remove(item);
                    return;
                }
                item.collectedByEnemy = false;
                item.gameObject.SetActive(false);
                _wallet.AddItem(item);
            }
        }
    }
}
