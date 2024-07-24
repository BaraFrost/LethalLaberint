using Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        private class CellWithWeight {

            public LabyrinthCell LabyrinthCell { get; private set; }
            public float weight;

            public CellWithWeight(LabyrinthCell labyrinthCell, float weight) {
                LabyrinthCell = labyrinthCell;
                this.weight = weight;
            }
        }

        [SerializeField]
        private EnemyContainer _enemyContainer;

        [SerializeField]
        private float _maxLabyrinthSize;

        private List<Enemy> _enemies = new List<Enemy>();

        private List<CellWithWeight> _cellsWithWeight;

        [SerializeField]
        private int _enemyCount;

        public List<Enemy> Spawn(PlayerController player, SpawnedLabyrinthCellsContainer labyrinthCells) {
            _cellsWithWeight = labyrinthCells.AvailableCells.Select(cell => new CellWithWeight(cell, 1f)).ToList();
            UpdateWeightsByDistance(player.transform.position);
            for (var i = 0; i < _enemyCount; i++) {
                _enemies.Add(SpawnRandomEnemy(player, labyrinthCells));
            }
            return _enemies;
        }

        private void UpdateWeightsByDistance(Vector3 position) {
            foreach (var cell in _cellsWithWeight) {
                if (_maxLabyrinthSize - Vector3.SqrMagnitude(position - cell.LabyrinthCell.transform.position) < 0) {
                    Debug.Log(_maxLabyrinthSize - Vector3.SqrMagnitude(position - cell.LabyrinthCell.transform.position));
                }
                cell.weight += 1 / Vector3.SqrMagnitude(position - cell.LabyrinthCell.transform.position);
            }
        }

        private Enemy SpawnRandomEnemy(PlayerController player, SpawnedLabyrinthCellsContainer labyrinthCells) {
            var enemyToSpawn = _enemyContainer.GetRandomEnemy();
            var positionToSpawn = GetRandomCellPosition();
            var enemy = Instantiate(enemyToSpawn, positionToSpawn, Quaternion.identity, transform);
            UpdateWeightsByDistance(enemy.transform.position);
            enemy.Init(player, labyrinthCells);
            return enemy;
        }

        private Vector3 GetRandomCellPosition() {
            var weightSum = 0f;
            foreach (var cell in _cellsWithWeight) {
                weightSum += cell.weight;
            }
            var randomPosition = Random.Range(0, weightSum);
            var currentWeightSum = 0f;
            for (int i = 0; i < _cellsWithWeight.Count; i++) {
                currentWeightSum += _cellsWithWeight[i].weight;
                if (currentWeightSum > randomPosition) {
                    var result = _cellsWithWeight[i].LabyrinthCell.gameObject.transform.position;
                    _cellsWithWeight.RemoveAt(i);
                    return result;
                }
            }
            return Vector3.zero;
        }
    }
}

