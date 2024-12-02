using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class WarehouseArea : MonoBehaviour {

        [SerializeField]
        private float _randomOffset;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioSource _particleSource;

        [SerializeField]
        private ParticleSystem _particleSystem;

        public void AddItems(List<CollectibleItem> collectibleItems) {
            if (collectibleItems != null && collectibleItems.Count > 0) {
                _audioSource.Play();
                _particleSystem.Play();
            }
            foreach (var item in collectibleItems) {
                item.transform.position = new Vector3(transform.position.x + Random.Range(0, _randomOffset), item.transform.position.y, transform.position.z + Random.Range(0, _randomOffset));
                item.DisableCollectibleItem();
                item.gameObject.SetActive(true);
            }
        }

        private void OnParticleCollision(GameObject other) {
          //  _particleSource.Play();
        }
    }
}

