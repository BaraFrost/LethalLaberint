namespace Game {

    public class AbstractVisualLogic<T> : AbstractEnemyLogic<T> where T : Enemy {

        public override void Init(Enemy enemy) {
            base.Init(enemy);
            if (Enemy.AttackLogic != null) {
                Enemy.AttackLogic.OnAttack += PlayAttackVisual;
                Enemy.AttackLogic.OnAttackStopped += PlayStopAttackVisual;
            }
        }

        public override void UpdateLogic() {
            base.UpdateLogic();
            PlayMoveLogicVisual();
        }

        private void PlayMoveLogicVisual() {
            if (Enemy.MovementLogic == null) {
                return;
            }
            if (Enemy.MovementLogic.IsMoving) {
                PlayMoveVisual();
                return;
            }
            PlayStopVisual();
            return;
        }

        protected virtual void PlayMoveVisual() { }

        protected virtual void PlayStopVisual() { }

        protected virtual void PlayAttackVisual() { }

        protected virtual void PlayStopAttackVisual() { }
    }
}
