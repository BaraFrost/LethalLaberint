using Data;
using Game;
using System;
using UnityEngine;

namespace UI {

    public class BookPopup : ClosablePopup {

        [SerializeField]
        private BooksScrollView _scrollView;

        [SerializeField]
        private BookItem _itemPrefab;

        [SerializeField]
        private BookItemsData _bookItemsData;

        private Enemy.EnemyType _scrollTargetType;

        protected override void Awake() {
            base.Awake();
            foreach (var itemData in _bookItemsData.BooksData) {
                var item = Instantiate(_itemPrefab);
                item.SetData(itemData);
                _scrollView.AddItem(item);
            }
        }

        public void SetData(Enemy.EnemyType scrollTargetType = Enemy.EnemyType.None) {
            _scrollTargetType = scrollTargetType;
        }

        public override void ShowPopup() {
            base.ShowPopup();
            for (var i = 0; i < _bookItemsData.BooksData.Length; i++) {
                _scrollView.SpawnedItems[i].SetData(_bookItemsData.BooksData[i]);
            }
            if (_scrollTargetType != Enemy.EnemyType.None) {
                _scrollView.ScrollToItem(Array.FindIndex(_bookItemsData.BooksData, data => data.enemyType == _scrollTargetType));
            }
        }
    }
}