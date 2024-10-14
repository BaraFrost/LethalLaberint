using Game;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(AbilityData), menuName = "Data/AbilityData")]
    public class AbilityData : ScriptableObject {

        [SerializeField]
        private AbstractAbility _abstractAbility;
        public AbstractAbility AbstractAbility => _abstractAbility;

        [SerializeField]
        private Sprite _sprite;
        public Sprite Sprite => _sprite;
    }
}
