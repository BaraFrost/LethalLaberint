using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(ShopItem), menuName = "Data/ShopItem")]
    public class ShopItem : ScriptableObject {

        public enum Type {
            Chest,
        }

        [SerializeField]
        private Type _type;
        public Type ItemType => _type;

        [SerializeField]
        private int _price;
        public int Price => _price;

        [SerializeField]
        private AbstractReward _reward;

        public void GiveReward() {
            _reward.GiveReward();
        }
    }
}
