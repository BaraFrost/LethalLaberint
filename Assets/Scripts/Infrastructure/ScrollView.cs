using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure {

    public class ScrollView<T> : MonoBehaviour where T : MonoBehaviour {

        [SerializeField]
        private ScrollRect _scrollRect;
        [SerializeField]
        private RectTransform _content;
        [SerializeField]
        private VerticalLayoutGroup _layoutGroup;
        [SerializeField]
        private float _scrollSpeed = 10f;

        private List<T> _spawnedItems = new List<T>();
        public List<T> SpawnedItems => _spawnedItems;
        private Vector2 _targetPosition;
        private bool _isScrolling = false;

        private void Update() {
            if (_isScrolling) {
                _content.anchoredPosition = Vector2.Lerp(_content.anchoredPosition, _targetPosition, Time.deltaTime * _scrollSpeed);

                if (Vector2.Distance(_content.anchoredPosition, _targetPosition) < 0.1f) {
                    _content.anchoredPosition = _targetPosition;
                    _isScrolling = false;
                }
            }
        }

        public void AddItem(T item) {
            item.transform.SetParent(_content.transform);
            _spawnedItems.Add(item);

            LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        }

        public void ScrollToItem(int index) {
            if (index < 0 || index >= _spawnedItems.Count) {
                Debug.LogError("ScrollView index out of range");
                return;
            }
            Canvas.ForceUpdateCanvases();
            var target = _spawnedItems[index].GetComponent<RectTransform>();
            var viewportLocalPos = _scrollRect.viewport.localPosition;

            _targetPosition = (Vector2)_scrollRect.transform.InverseTransformPoint(_content.position) - (Vector2)_scrollRect.transform.InverseTransformPoint(target.position);
            _isScrolling = true;
        }

        public void ClearItems() {
            foreach (var item in _spawnedItems) {
                Destroy(item);
            }
            _spawnedItems.Clear();
        }
    }
}
