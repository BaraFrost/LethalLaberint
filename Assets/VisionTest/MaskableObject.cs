using UnityEngine;

namespace Game {

    public class MaskableObject : MonoBehaviour {

        private void Start() {
            var materials = GetComponent<MeshRenderer>().materials;
            for(int i = 0; i < materials.Length; i++) {
                materials[i].renderQueue = 3002;
            }
        }
    }
}

