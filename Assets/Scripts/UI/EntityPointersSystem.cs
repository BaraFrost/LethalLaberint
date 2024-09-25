using Game;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class EntityPointersSystem : MonoBehaviour {

        [SerializeField]
        private Camera _gameCamera;

        [SerializeField]
        private RectTransform _enemyPointerPrefab;

        [SerializeField]
        private RectTransform _itemPointerPrefab;

        private List<RectTransform> _itemsPointers = new List<RectTransform>();
        private List<RectTransform> _enemyPointers = new List<RectTransform>();

        private List<Enemy> _enemies;
        private List<CollectibleItem> _collectibleItems;

        public void Init(List<Enemy> enemies, List<CollectibleItem> collectibleItem) {
            _enemies = enemies;
            _collectibleItems = collectibleItem;
            foreach (var enemy in enemies) {
                _enemyPointers.Add(Instantiate(_enemyPointerPrefab, transform));
            }
            foreach (var item in collectibleItem) {
                // _itemsPointers.Add(Instantiate(_itemPointerPrefab, transform));
            }
        }

        private void Update() {
            for (int i = 0; i < _itemsPointers.Count; i++) {
                UpdateArrowPosition(_collectibleItems[i].transform, _itemsPointers[i]);
            }
            for (int i = 0; i < _enemyPointers.Count; i++) {
                if (_enemies[i] == null) {
                    _enemyPointers[i].gameObject.SetActive(false);
                    continue;
                }
                UpdateArrowPosition(_enemies[i].transform, _enemyPointers[i]);
            }
        }

        private void UpdateArrowPosition(Transform entity, RectTransform pointer) {
            var screenPosition = _gameCamera.WorldToScreenPoint(entity.position);
            var isOffScreen = screenPosition.z < 0 || screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height;
            if (isOffScreen) {
                pointer.gameObject.SetActive(true);

                var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
                var direction = ((Vector2)screenPosition - screenCenter).normalized;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                pointer.rotation = Quaternion.Euler(0, 0, angle - 90);
                var enemyDirection = ((Vector2)screenPosition - screenCenter).normalized;
                pointer.position = GetArrowPositionAtEdge(screenCenter, enemyDirection);
            } else {
                pointer.gameObject.SetActive(false);
            }
        }

        private Vector2 GetArrowPositionAtEdge(Vector2 screenCenter, Vector2 direction) {
            var x = screenCenter.x + direction.x * Screen.width / 2;
            var y = screenCenter.y + direction.y * Screen.height / 2;

            var aspectRatio = Screen.width / Screen.height;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                x = direction.x > 0 ? Screen.width - 50 : 50;
                y = screenCenter.y + (x - screenCenter.x) / direction.x * direction.y;
            } else {
                y = direction.y > 0 ? Screen.height - 50 : 50;
                x = screenCenter.x + (y - screenCenter.y) / direction.y * direction.x;
            }

            return new Vector2(x, y);
        }
    }
}
