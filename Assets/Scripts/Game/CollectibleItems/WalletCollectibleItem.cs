using UnityEngine;

namespace Game {

    public class WalletCollectibleItem : MonoBehaviour {

        [SerializeField]
        private int _price;
        public int Price => _price;
    }
}
