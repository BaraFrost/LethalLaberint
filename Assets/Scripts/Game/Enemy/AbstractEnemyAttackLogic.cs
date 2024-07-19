using UnityEngine;

namespace Game {

    public abstract class AbstractEnemyAttackLogic : MonoBehaviour {

        public abstract bool CanAttackTarget(PlayerController target);

        public abstract void AttackTarget(PlayerController target);
    }
}

