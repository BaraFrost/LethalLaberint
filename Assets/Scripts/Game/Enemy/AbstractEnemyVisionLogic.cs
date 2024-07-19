using UnityEngine;

namespace Game {

    public abstract class AbstractEnemyVisionLogic : MonoBehaviour {

        public abstract bool CanSeeTarget(PlayerController enemyTarget);
    }
}

