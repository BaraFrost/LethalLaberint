using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class EnemyItemsCollectionLogic : AbstractEnemyLogic<Enemy> {

        public bool HasItem => _collectedItem != null;
        public Vector3 StartPosition { get; private set; }
        public bool MaxItemsCollected => _collectedItemsCount >= _maxItemsToCollectCount;

        [SerializeField]
        private float _searchDistance;
        [SerializeField]
        private LayerMask _raycastLayerMask;
        [SerializeField]
        private Transform _visionPosition;
        [SerializeField]
        private int _maxItemsToCollectCount;
        [SerializeField]
        private GameObject _visualItem;
        [SerializeField]
        private MeshRenderer _visualItemRenderer;
        [SerializeField]
        private MeshFilter _visualItemFilter;

        private int _collectedItemsCount;
        private List<CollectibleItem> _itemsInZone = new List<CollectibleItem>();
        private CollectibleItem _collectedItem;
        public Action OnItemDropped;

        private void Awake() {
            if (_visualItem != null) {
                _visualItem.SetActive(false);
            }
            StartPosition = transform.position;
        }

        public bool TryToFindItem(List<CollectibleItem> collectibleItems, out CollectibleItem availableItem) {
            availableItem = null;
            _itemsInZone.Clear();
            foreach (var item in collectibleItems) {
                if (item.collectedByEnemy || item.Collected || !item.gameObject.activeSelf) {
                    continue;
                }
                if (Vector3.Distance(item.transform.position, transform.position) <= _searchDistance) {
                    _itemsInZone.Add(item);
                }
            }
            if (_itemsInZone.Count == 0) {
                return false;
            }
            availableItem = GetVisibleItem();
            if (availableItem == null) {
                return false;
            }
            return true;
        }

        private CollectibleItem GetVisibleItem() {
            CollectibleItem availableItem = null;
            var minDistance = float.MaxValue;
            foreach (var item in _itemsInZone) {
                var directionVector = item.transform.position - _visionPosition.transform.position;
                var currentDistance = directionVector.sqrMagnitude;
                if (Physics.Raycast(_visionPosition.position, directionVector, out var hitInfo, _searchDistance, _raycastLayerMask) && hitInfo.collider != null
                    && hitInfo.collider.gameObject == item.gameObject && currentDistance < minDistance) {
                    minDistance = currentDistance;
                    availableItem = item;
                }
            }
            return availableItem;
        }

        public void CollectItem(CollectibleItem item) {
            if (!item.gameObject.activeSelf || item.Collected) {
                return;
            }
            item.collectedByEnemy = true;
            item.gameObject.SetActive(false);
            _collectedItem = item;
            if (_visualItem != null) {
                _visualItem.SetActive(true);
                _visualItemRenderer.material = item.MeshRenderer.sharedMaterial;
                _visualItemFilter.mesh = item.MeshFilter.sharedMesh;
                _visualItem.transform.localScale = item.transform.localScale;
            }
        }

        public void DropCollectibleItem() {
            _collectedItemsCount++;
            _collectedItem.transform.position = new Vector3(transform.position.x, _collectedItem.transform.position.y, transform.position.z);
            _collectedItem.gameObject.SetActive(true);
            _collectedItem.Drop();
            _collectedItem = null;
            if (_visualItem != null) {
                _visualItem.SetActive(false);
            }
            OnItemDropped?.Invoke();
        }
    }
}
