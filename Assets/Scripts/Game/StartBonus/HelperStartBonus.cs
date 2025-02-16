using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = nameof(HelperStartBonus), menuName = "Data/HelperStartBonus")]
    public class HelperStartBonus : AbstractStartBonus {

        [SerializeField]
        private HelperEnemy _helperEnemyPrefab;

        public override void Apply(GameEntitiesContainer entitiesContainer) {
            var helper = Instantiate(_helperEnemyPrefab, entitiesContainer.cellsContainer.StartCells.StartCell.transform.position, Quaternion.identity);
            helper.Init(entitiesContainer);
            entitiesContainer.enemies.Add(helper);
        }
    }
}