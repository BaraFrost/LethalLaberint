using System;

namespace Game {

    public abstract class AbstractEnemyAttackLogic : AbstractEnemyLogic<Enemy>{

        public Action OnAttack;
        public Action OnAttackStopped;

        public virtual bool IsAttacking => false;

        public abstract bool CanAttackTarget(PlayerController target);

        public abstract void AttackTarget(PlayerController target);

        public abstract void StopAttack();
    }
}

