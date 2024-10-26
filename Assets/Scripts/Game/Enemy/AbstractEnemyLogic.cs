using UnityEngine;

namespace Game {

    public class AbstractEnemyLogic<T> : MonoBehaviour where T : Enemy {

        private T _enemy;
        public T Enemy => _enemy;

        public virtual void Init(T enemy) {
            _enemy = enemy;
        }
    }
}
