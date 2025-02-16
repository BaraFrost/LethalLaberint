using Infrastructure;
using UnityEngine;

namespace Game {

    public abstract class AbstractStartBonus : ScriptableObject {

        [SerializeField]
        private LocalizationText _name;
        public LocalizationText Name => _name;

        [SerializeField]
        private Sprite _icon;
        public Sprite Icon => _icon;

        public abstract void Apply(GameEntitiesContainer entitiesContainer);
    }
}
