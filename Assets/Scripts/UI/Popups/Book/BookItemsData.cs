using Game;
using Infrastructure;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data {

    [CreateAssetMenu(fileName = nameof(BookItemsData), menuName = "Data/BookItemsData")]
    public class BookItemsData : ScriptableObject {

        [Serializable]
        public struct Data {
            public Enemy.EnemyType enemyType;
            public AssetReference enemyImageAssetReference;
            public LocalizationText title;
            public LocalizationText description;
        }

        [SerializeField]
        private Data[] _booksData;
        public Data[] BooksData => _booksData;
    }
}
