using Game;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI {

    public class EntityPointersSystem : MonoBehaviour {

        [SerializeField]
        private Camera _gameCamera;

        [SerializeField]
        private RectTransform _enemyPointerPrefab;

        [SerializeField]
        private RectTransform _itemPointerPrefab;

        [SerializeField]
        private float _minSizeDistance;
        [SerializeField]
        private float _maxSizeDistance;
        [SerializeField]
        private float _minSize;

        private List<RectTransform> _itemsPointers = new List<RectTransform>();
        private List<RectTransform> _enemyPointers = new List<RectTransform>();

        private GameEntitiesContainer _entitiesContainer;
        private List<Enemy> Enemies => _entitiesContainer.enemies;
        private PlayerController PlayerController => _entitiesContainer.playerController;

        public void Init(GameEntitiesContainer gameEntitiesContainer) {
            _entitiesContainer = gameEntitiesContainer;
            foreach (var enemy in Enemies) {
                _enemyPointers.Add(Instantiate(_enemyPointerPrefab, transform));
            }
        }

        private void Update() {
            if (Enemies.Count > _enemyPointers.Count) {
                for (var i = _enemyPointers.Count; i < Enemies.Count; i++) {
                    _enemyPointers.Add(Instantiate(_enemyPointerPrefab, transform));
                }
            }
            for (int i = 0; i < _enemyPointers.Count; i++) {
                if (Enemies.Count <= i || Enemies[i] == null || !Enemies[i].NeedShowArrow) {
                    _enemyPointers[i].gameObject.SetActive(false);
                    continue;
                }
                UpdateArrowPosition(Enemies[i].transform, _enemyPointers[i], _enemyPointerPrefab);
            }
        }

        private void UpdateArrowPosition(Transform entity, RectTransform pointer, RectTransform prefab) {
            var distanceToEntity = (PlayerController.transform.position - entity.position).magnitude;
            if (distanceToEntity > _minSizeDistance) {
                pointer.gameObject.SetActive(false);
                return;
            }
            var arrowSize = _enemyPointerPrefab.localScale * (1 - (distanceToEntity - _maxSizeDistance) / (_minSizeDistance - _maxSizeDistance));
            if (arrowSize.magnitude < _minSize) {
                pointer.gameObject.SetActive(false);
                return;
            }
            pointer.localScale = arrowSize;
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
