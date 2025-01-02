
namespace Game {

    public class BlackoutAbility : AbstractAbility {

        public override void Activate() {
            base.Activate();
            var enemies = _player.GameEntitiesContainer.enemies;
            foreach (var enemy in enemies) {
                if (enemy.Type == Enemy.EnemyType.Electric && enemy.VisionLogic != null) {
                    enemy.VisionLogic.TemporarilyDisable(AbilityTime);
                }
            }
            _player.PlayerVisualLogic.EnvironmentVisualLogic.LightLogic.ActivateDarkLight();
            ChangeDoorActiveState(false);
        }

        protected override void Stop() {
            base.Stop();
            ChangeDoorActiveState(true);
            _player.PlayerVisualLogic.EnvironmentVisualLogic.LightLogic.ActivateDefaultLight();
        }

        private void ChangeDoorActiveState(bool enabled) {
            var doors = _player.GameEntitiesContainer.cellsContainer.CellsWithDoors;
            foreach (var door in doors) {
                door.disableDoor = !enabled;
            }
        }
    }
}
