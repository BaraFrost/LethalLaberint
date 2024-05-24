using UnityEngine;

namespace Game {

    public class GameLoader : MonoBehaviour {

        [SerializeField]
        private LabyrinthSpawner _labyrinthSpawner;

        private void Awake() {
            _labyrinthSpawner.Spawn();
        }
    }
}
