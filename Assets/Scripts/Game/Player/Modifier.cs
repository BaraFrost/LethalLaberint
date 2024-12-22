using NaughtyAttributes;
using UnityEngine;

namespace Data {

    public enum ModifierType {
        Speed,
        Wallet,
        Money
    }

    [CreateAssetMenu(fileName = nameof(Modifier), menuName = "Data/Modifier")]
    public class Modifier : ScriptableObject {

        [SerializeField]
        private ModifierType _type;
        public ModifierType Type => _type;

        [SerializeField]
        private float _value;
        public virtual float Value => _value;

        [SerializeField]
        private bool _temporary;
        public bool Temporary => _temporary;

        [SerializeField]
        [ShowIf(nameof(_temporary))]
        private float _modifierTime;
        public float ModifierTime => _modifierTime;

        [SerializeField]
        private bool _showModifierEffect;
        public bool ShowModifierEffect => _showModifierEffect;
    }
}

