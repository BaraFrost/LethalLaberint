using UnityEngine;

namespace Game {

    public abstract class AbstractAbility : MonoBehaviour {

        protected PlayerController _player;

        [SerializeField]
        private float _time;
        public virtual float AbilityTime => _time;

        private float _currentTime;
        public float Progress => Mathf.Min(_currentTime / AbilityTime, 1);
        public virtual bool IsAbilityActive => _abilityActive;
        private bool _abilityActive;
        public bool CanUseAbility => !IsAbilityActive && !_player.HealthLogic.IsDamaged;

        public virtual void Init(PlayerController playerController) {
            _player = playerController;
            _player.HealthLogic.onDamaged += OnPlayerDamaged;
        }

        public virtual void Activate() {
            if(_abilityActive) {
                return;
            }
            _abilityActive = true;
            _currentTime = 0;
        }

        private void Update() {
            if(!_abilityActive) {
                return;
            }
            _currentTime += Time.deltaTime;
            if (_currentTime > _time) {
                Stop();
            }
        }

        protected virtual void Stop() {
            _abilityActive = false;
        }

        protected virtual void OnPlayerDamaged() { }
    }
}
