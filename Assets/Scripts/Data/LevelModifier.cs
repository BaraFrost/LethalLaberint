using UnityEngine;

namespace Data {
    
    [CreateAssetMenu(fileName = nameof(LevelModifier), menuName = "Data/LevelModifier")]
    public class LevelModifier : Modifier {

        [SerializeField]
        private float _increasePerLevel;
        public float IncreasePerLevelValue => _increasePerLevel;

        public override float Value => GetValueByLevel();

        private float GetValueByLevel() {
            if (!Account.Instance.ModifiersCountData.TryGetValue(Type,out var level)){
                return base.Value;
            }
            return base.Value + _increasePerLevel * level;
        }
    }
}

