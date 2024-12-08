using Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game {
    public class EnvironmentSpawnLogic : MonoBehaviour {

        [SerializeField]
        private EnvironmentContainer _environmentContainer;

        public void Spawn(SpawnedLabyrinthCellsContainer labyrinthCellsContainer) {
            var cells = labyrinthCellsContainer.AvailableCells;
            var environmentCount = cells.Count * _environmentContainer.MaxEnvironmentPercent;
            var spawnedEnvironmentCount = new Dictionary<EnvironmentContainer.CellsEnvironmentWithPercent, int>();
            var cellsEnvironment = _environmentContainer.CellsEnvironments.ToList();
            var availableEnvironments = GetAvailableEnvironment(cellsEnvironment, cells, spawnedEnvironmentCount);
            while (spawnedEnvironmentCount.Values.Sum() < environmentCount && availableEnvironments.Count > 0) {
                var randomEnvironment = availableEnvironments[Random.Range(0, availableEnvironments.Count)];
                var availableCells = cells.Where(cell => cell.AvailableOnlyThisDirectionsDefault(randomEnvironment.environment.Directions)).ToArray();
                if (availableCells.Length == 0) {
                    cellsEnvironment.Remove(randomEnvironment);
                    availableEnvironments.Remove(randomEnvironment);
                    continue;
                }
                var randomCellIndex = Random.Range(0, availableCells.Length);
                var randomCell = availableCells[randomCellIndex];
                var spawnedEnvironment = Instantiate(randomEnvironment.environment, randomCell.transform.position, randomCell.transform.rotation, randomCell.transform);
                if (!spawnedEnvironmentCount.ContainsKey(randomEnvironment)) {
                    spawnedEnvironmentCount[randomEnvironment] = 0;
                }
                spawnedEnvironmentCount[randomEnvironment]++;
                cells.Remove(randomCell);
                availableEnvironments = GetAvailableEnvironment(cellsEnvironment, cells, spawnedEnvironmentCount);
            }
        }

        private List<EnvironmentContainer.CellsEnvironmentWithPercent> GetAvailableEnvironment(List<EnvironmentContainer.CellsEnvironmentWithPercent> cellsEnvironment, List<LabyrinthCell> cells, Dictionary<EnvironmentContainer.CellsEnvironmentWithPercent, int> spawnedEnvironmentCount) {
            return cellsEnvironment.Where(cellsWithPercent => !spawnedEnvironmentCount.ContainsKey(cellsWithPercent)
            || Mathf.Ceil(cellsWithPercent.percent * cells.Count) > spawnedEnvironmentCount[cellsWithPercent]).ToList();
        }
    }
}
