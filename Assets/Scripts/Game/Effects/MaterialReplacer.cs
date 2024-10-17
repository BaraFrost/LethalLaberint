using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class MaterialReplacer : MonoBehaviour {

        [SerializeField]
        private Material _defaultMaterial;
        [SerializeField]
        private Material _activationMaterial;

        [SerializeField]
        private MeshRenderer[] _renderers;

        private bool _active = false;

        private void Awake() {
            SetMaterial(_defaultMaterial);
        }

        public void Switch() {
            SetMaterial(_active ? _defaultMaterial : _activationMaterial);
            _active = !_active;
        }

        private void SetMaterial(Material material) {
            for (var i = 0; i < _renderers.Length; i++) {
                _renderers[i].material = material;
            }
        }
    }
}
