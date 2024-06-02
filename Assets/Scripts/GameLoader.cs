using NaughtyAttributes;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

namespace Game {

    public class GameLoader : MonoBehaviour {

        [SerializeField]
        private LabyrinthSpawner _labyrinthSpawner;

        [SerializeField]
        private NavMeshSurface _navMeshSurface;

        private void Awake() {
            Init();
        }

        private void Init() {
            _labyrinthSpawner.Spawn();
            _navMeshSurface.BuildNavMesh();
        }

        [Button]
        private void Restart() {
            if(!EditorApplication.isPlaying) {
                return;
            }
            Init();
        }
    }
}
