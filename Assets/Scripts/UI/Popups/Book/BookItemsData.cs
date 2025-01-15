using Game;
using Infrastructure;
using System;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(BookItemsData), menuName = "Data/BookItemsData")]
    public class BookItemsData : ScriptableObject {

        [Serializable]
        public struct Data {
            public Enemy.EnemyType enemyType;
            public Sprite enemyImage;
            public LocalizationText title;
            public LocalizationText description;
        }

        [SerializeField]
        private Data[] _booksData;
        public Data[] BooksData => _booksData;
    }
}
