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

        private DifficultyProgressionConfig _difficultyProgressionConfig;

        private float _countMult;

        public List<Enemy> Spawn(PlayerController player, SpawnedLabyrinthCellsContainer labyrinthCells, List<CollectibleItem> collectibleItems, DifficultyProgressionConfig difficultyProgressionConfig) {
            _difficultyProgressionConfig = difficultyProgressionConfig;
            if (labyrinthCells.StartCells.NeedGenerateEnemy) {
                _cellsWithWeight = labyrinthCells.AvailableCells.Select(cell => new CellWithWeight(cell, 1f)).ToList();
                UpdateWeightsByDistance(player.transform.position);
                var enemiesToSpawn = _enemyContainer.GetRandomEnemies(_difficultyProgressionConfig.EnemyCount, _difficultyProgressionConfig.CurrentDifficultyStage);
                for (var i = 0; i < enemiesToSpawn.Count; i++) {
                    _enemies.Add(SpawnRandomEnemy(enemiesToSpawn[i]));
                }
            }
            _enemies.AddRange(labyrinthCells.StartCells.StartEnemies);
            foreach (var startEnemy in _enemies) {
                startEnemy.Init(player, labyrinthCells, collectibleItems);
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

        private Enemy SpawnRandomEnemy(Enemy enemyToSpawn) {
            var positionToSpawn = GetRandomCellPosition();
            positionToSpawn.y = enemyToSpawn.transform.position.y;
            var enemy = Instantiate(enemyToSpawn, positionToSpawn, Quaternion.identity, transform);
            UpdateWeightsByDistance(enemy.transform.position);
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
                    var result = _cellsWithWeight[i].LabyrinthCell.transform.position;
                    _cellsWithWeight[i].LabyrinthCell.cellBusy = true;
                    _cellsWithWeight.RemoveAt(i);
                    return result;
                }
            }
            return Vector3.zero;
        }
    }
}

