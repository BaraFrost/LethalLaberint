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
        }

        private void UpdateHealth(Enemy.EnemyType type) {
            for (var i = 0; i < _healthImages.Count - _healthLogic.HealthCount; i++) {
                _healthImages[i].gameObject.SetActive(false);
            }
        }
    }
}

