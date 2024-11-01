using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game {

    public class RagdollVisualLogic : MonoBehaviour {

        [SerializeField]
        private Transform _boneRoot;

        [SerializeField]
        private CollectibleItem _ragdollPrefab;

        public List<CollectibleItem> SpawnedRagdolls { get; private set; } = new List<CollectibleItem>();

        private Dictionary<string, Transform> _bones = new Dictionary<string, Transform>();

        private void Awake() {
            _bones = _boneRoot.GetComponentsInChildren<Transform>().ToDictionary(transform => transform.name);
        }

        public void SpawnRagdoll() {
            var ragdoll = Instantiate(_ragdollPrefab, transform.position, transform.rotation, transform.parent);
            SpawnedRagdolls.Add(ragdoll);
            var ragdollBones = ragdoll.GetComponentsInChildren<Transform>();
            foreach(var bone in ragdollBones) {
                if(!_bones.ContainsKey(bone.name)) {
                    continue;
                }
                var playerBone = _bones[bone.name];
                bone.position = playerBone.position;
                bone.rotation = playerBone.rotation;
            }
        }
    }
}
