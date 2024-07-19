using System;
using UnityEngine;

namespace Game {

    public class WalletItemDropPanel : MonoBehaviour {

        public Action<WalletCollectibleItem> onItemDropped;

        private void OnCollisionEnter2D(Collision2D collision) {
            if(collision.gameObject.TryGetComponent<WalletCollectibleItem>(out var item)) {
                onItemDropped?.Invoke(item);
            }
        }
    }
}
