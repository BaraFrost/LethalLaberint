namespace Game {

    public abstract class AbstractEnemyAttackLogic : AbstractEnemyLogic<Enemy>{

        public abstract bool CanAttackTarget(PlayerController target);

        public abstract void AttackTarget(PlayerController target);

        public abstract void StopAttack();
    }
}

