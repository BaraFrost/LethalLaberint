using System.Collections.Generic;

namespace Game {

    public class GameEntitiesContainer {

        public PlayerController playerController;
        public SpawnedLabyrinthCellsContainer cellsContainer;
        public List<Enemy> enemies;
        public List<CollectibleItem> collectibleItems;
        public AdditionalEnemyTarget additionalEnemyTarget; 
    }
}
