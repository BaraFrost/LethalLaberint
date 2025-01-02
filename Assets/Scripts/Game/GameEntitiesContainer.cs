using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class GameEntitiesContainer : MonoBehaviour {

        [NonSerialized]
        public PlayerController playerController;
        [NonSerialized]
        public SpawnedLabyrinthCellsContainer cellsContainer;
        [NonSerialized]
        public List<Enemy> enemies;
        [NonSerialized]
        public List<CollectibleItem> collectibleItems;
        [NonSerialized]
        public AdditionalEnemyTarget additionalEnemyTarget;


        private void Update() {
            if (playerController != null) {
                playerController.UpdateLogics();
            }
            for (int i = 0; i < enemies.Count; i++) {
                if (enemies[i] != null) {
                    enemies[i].UpdateEnemy();
                }
            }
        }
    }
}
