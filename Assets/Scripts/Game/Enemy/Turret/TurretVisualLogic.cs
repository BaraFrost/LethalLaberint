using System.Linq;
using UnityEngine;

namespace Game {

    public class TurretVisualLogic : MonoBehaviour {

        [SerializeField]
        private LineRenderer _laserLineRenderer;

        [SerializeField]
        private Transform _laserStart;

        [SerializeField]
        private ParticleSystem[] _shootParticles;

        [SerializeField]
        private ParticleSystem[] _prepareShootParticles;

        private Vector3[] _points = new Vector3[2];

        private void FixedUpdate() {
            if(Physics.Raycast(_laserStart.position, _laserStart.forward,out var hit)) {
                _points[0] = _laserStart.position;
                _points[1] = hit.point;
                _laserLineRenderer.SetPositions(_points);
            }
        }

        public void PlayShootEffects() {
            foreach (var effect in _shootParticles) {
                effect.Play();
            }
        }

        public void PlayPrepareShootEffects() {
            foreach (var effect in _prepareShootParticles) {
                effect.Play();
            }
        }

        public void StopShootEffects() {
            foreach (var effect in _shootParticles.Concat(_prepareShootParticles)) {
                effect.Stop();
            }
        }
    }
}

