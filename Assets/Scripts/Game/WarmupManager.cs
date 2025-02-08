using System.Collections;
using UnityEngine;

namespace Game {
    public class WarmupManager : MonoBehaviour {

        [SerializeField]
        private ParticleSystem[] _particlesToWarmup;

        [SerializeField]
        private AudioClip[] _clipsToWarmup;

        [SerializeField]
        private AudioSource _audioSource;

        private void Awake() {
            foreach (var particle in _particlesToWarmup) {
                var ps = Instantiate(particle, transform.position, Quaternion.identity, transform);
                ps.Simulate(0.5f, true, true);
                ps.Play();
                ps.Stop();
                ps.gameObject.SetActive(false);
            }
            StartCoroutine(WarmupAudio());
        }

        private IEnumerator WarmupAudio() {
            float originalVolume = _audioSource.volume;
            _audioSource.volume = 0f;

            foreach (var clip in _clipsToWarmup) {
                _audioSource.clip = clip;
                _audioSource.Play();
                // Дождаться хотя бы одного кадра, чтобы система успела обработать воспроизведение
                yield return null;
                _audioSource.Stop();
            }

            _audioSource.volume = originalVolume;
        }
    }
}

