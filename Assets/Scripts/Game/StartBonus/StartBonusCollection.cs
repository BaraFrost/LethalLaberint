using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = nameof(StartBonusCollection), menuName = "Data/StartBonusCollection")]
    public class StartBonusCollection : ScriptableObject {

        [SerializeField]
        private AbstractStartBonus[] _startBonuses;
        public AbstractStartBonus[] StartBonuses => _startBonuses;
    }
}