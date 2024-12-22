using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(LevelsModifiersContainer), menuName = "Data/LevelsModifiersContainer")]
    public class LevelsModifiersContainer : ScriptableObject {

        [SerializeField]
        private List<LevelModifier> _levelModifiers;

        private Dictionary<ModifierType, LevelModifier> _cachedModifiers;
        public Dictionary<ModifierType, LevelModifier> Modifiers { 
            get { 
                if(_cachedModifiers == null) {
                    _cachedModifiers = _levelModifiers.ToDictionary(modifier => modifier.Type, modifier => modifier);
                }
                return _cachedModifiers;
            } 
        }
    }
}
