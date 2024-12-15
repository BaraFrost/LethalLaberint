using System.Collections;
using UnityEngine;

namespace Game {

    public class BlackoutAbility : AbstractAbility {

        [SerializeField]
        private float _effectTime;

        private bool _isActive;
        public override bool IsAbilityActive => _isActive;

        public override void Activate() {
            if (_isActive) {
                return;
            }
            StartCoroutine(BlackoutCoroutine());
        }

        private IEnumerator BlackoutCoroutine() {
            _isActive = true;
            var enemies = _player.GameEntitiesContainer.enemies;
            foreach (var enemy in enemies) {
                if (enemy.Type == Enemy.EnemyType.Electric && enemy.VisionLogic != null) {
                    enemy.VisionLogic.TemporarilyDisable(_effectTime);
                }
            }
            _player.PlayerVisualLogic.EnvironmentVisualLogic.LightLogic.ActivateDarkLight();
            ChangeDoorActiveState(false);
            yield return new WaitForSeconds(_effectTime);
            ChangeDoorActiveState(true);
            _player.PlayerVisualLogic.EnvironmentVisualLogic.LightLogic.ActivateDefaultLight();
            _isActive = false;
        }

        private void ChangeDoorActiveState(bool enabled) {
            var doors = _player.GameEntitiesContainer.cellsContainer.CellsWithDoors;
            foreach (var door in doors) {
                door.disableDoor = !enabled;
            }
        }
    }
}
