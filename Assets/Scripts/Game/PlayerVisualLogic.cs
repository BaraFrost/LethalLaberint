using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game {

    public class PlayerVisualLogic : MonoBehaviour {

        [SerializeField]
        private Transform _playerBoneRoot;

        [SerializeField]
        private GameObject _playerRagdoll;

        private Dictionary<string, Transform> _bones = new Dictionary<string, Transform>();

        private void Awake() {
            _bones = _playerBoneRoot.GetComponentsInChildren<Transform>().ToDictionary(transform => transform.name);
        }

        public void SpawnRagdoll() {
            var ragdoll = Instantiate(_playerRagdoll, transform.position, transform.rotation, transform.parent);
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
