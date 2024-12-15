using UnityEngine;

namespace Game {

    public class AbstractEnemyLogic<T> : MonoBehaviour, IEnemyLogic where T : Enemy {

        private T _enemy;
        protected T Enemy => _enemy;

        public virtual void Init(Enemy enemy) {
            _enemy = enemy as T;
        }

        public virtual void UpdateLogic() {}
    }
}
