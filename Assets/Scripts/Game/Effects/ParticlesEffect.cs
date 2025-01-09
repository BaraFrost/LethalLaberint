using UnityEngine;

namespace Game {

    public class ParticlesEffect : MonoBehaviour {

        [SerializeField]
        private ParticleSystem[] _particleSystems;

        public void Play() {
            foreach (var particleSystem in _particleSystems) {
                particleSystem.Play();
            }
        }

        public void Stop() {
            foreach (var particleSystem in _particleSystems) {
                particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}

