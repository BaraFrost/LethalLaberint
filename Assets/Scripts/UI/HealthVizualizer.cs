using Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class HealthVisualizer : MonoBehaviour {

        [SerializeField]
        private HorizontalLayoutGroup _contentGroup;

        [SerializeField]
        private Image _healthImagePrefab;

        [SerializeField]
        private HealthLogic _healthLogic;

        private List<Image> _healthImages = new List<Image>();

        private void Start() {
            for (var i = 0; i < _healthLogic.HealthCount; i++) {
                var healthImage = Instantiate(_healthImagePrefab, _contentGroup.transform);
                _healthImages.Add(healthImage);
            }
            _healthLogic.onDamaged += UpdateHealth;
            _healthLogic.onRevive += UpdateHealth;
        }

        private void UpdateHealth(Enemy.EnemyType type) {
            UpdateHealth();
        }

        private void UpdateHealth() {
            if(_healthLogic.HealthCount > _healthImages.Count) {
                var currentHealthImagesCount = _healthImages.Count;
                for (var i = 0; i < _healthLogic.HealthCount - currentHealthImagesCount; i++) {
                    var healthImage = Instantiate(_healthImagePrefab, _contentGroup.transform);
                    _healthImages.Add(healthImage);
                }
            }
            for (var i = 0; i < _healthImages.Count; i++) {
                _healthImages[i].gameObject.SetActive(i < _healthLogic.HealthCount);
            }
        }
    }
}

