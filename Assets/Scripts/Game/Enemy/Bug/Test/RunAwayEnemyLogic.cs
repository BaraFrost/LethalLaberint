using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game {

    public class RunAwayEnemyLogic : AbstractEnemyLogic<Enemy> {

        public float _searchPointDistance;
        public Transform _visionPoint;
        public LayerMask _raycastMask;
        public float _minAngleToDestination;
        public float _cellsSize = 2.485378f;

       /* public List<Vector3> GetAvailablePoints(Vector3 excludedDirection) {
            var result = new List<Vector3>();
            TryToAddAvailablePoint(Vector3.forward, excludedDirection, result);
            TryToAddAvailablePoint(Vector3.back, excludedDirection, result);
            TryToAddAvailablePoint(Vector3.left, excludedDirection, result);
            TryToAddAvailablePoint(Vector3.right, excludedDirection, result);
            TryToAddAvailablePoint(Vector3.forward + Vector3.left, excludedDirection, result);
            TryToAddAvailablePoint(Vector3.back + Vector3.left, excludedDirection, result);
            TryToAddAvailablePoint(Vector3.back + Vector3.right, excludedDirection, result);
            TryToAddAvailablePoint(Vector3.forward + Vector3.right, excludedDirection, result);
            return result;
        }*/

       /* private bool TryToAddAvailablePoint(Vector3 direction, Vector3 excludedDirection, List<Vector3> result) {
            if (Vector3.Angle(excludedDirection, direction) < _minAngleToDestination || Physics.Raycast(_visionPoint.position, direction, _searchPointDistance, _raycastMask)) {
                return false;
            }
            result.Add(WorldToGrid(direction * _searchPointDistance + gameObject.transform.position));
            return true;
        }*/

        private Vector3 WorldToGrid(Vector3 worldPosition) {
            float x = Mathf.Round(worldPosition.x / _cellsSize) * _cellsSize;
            float z = Mathf.Round(worldPosition.z / _cellsSize) * _cellsSize;

            return new Vector3(x, worldPosition.y, z);
        }

        public float safeDistance = 0.1f; // –ассто€ние, на которое противник будет пытатьс€ убежать
        public float safeDistance2 = 1f; // –ассто€ние, на которое противник будет пытатьс€ убежать
        [SerializeField]
        private NavMeshAgent agent;
        private static System.Random rng = new System.Random();

        public bool TryToRunsAway(Vector3 enemyPosition) {
            var directions = new List<Vector3>() {
             Vector3.forward,
             Vector3.back,
             Vector3.right,
             Vector3.left,
             Vector3.forward + Vector3.right,
             Vector3.forward + Vector3.left,
             Vector3.back + Vector3.right,
             Vector3.back + Vector3.left,
            };
            var directionToPlayer = enemyPosition - transform.position;
            directionToPlayer.y = 0;
            var resultPositions = new List<Vector3>();
            foreach (var direction in directions/*.OrderBy(_ => rng.Next()).ToList()*/) {
                if (TryToRunAway(direction, directionToPlayer, out var position)) {
                    var path = new NavMeshPath();
                    agent.CalculatePath(position, path);
                    if (path.status != NavMeshPathStatus.PathComplete) {
                        continue;
                    }
                    agent.isStopped = false;
                    agent.SetDestination(position);
                    return true;
                  //  resultPositions.Add(position);
                }
            }
           /* foreach (var position in resultPositions.OrderByDescending(value => (transform.position - value).sqrMagnitude)) {
                var path = new NavMeshPath();
                agent.CalculatePath(position, path);
                if (path.status != NavMeshPathStatus.PathComplete) {
                    continue;
                }
                agent.isStopped = false;
                agent.SetDestination(position);
                return true;
            }*/
            agent.isStopped = true;
            return false;
        }
        private bool TryToRunAway(Vector3 direction, Vector3 excludedDirection, out Vector3 position) {
            position = Vector3.zero;
            if (Vector3.Angle(excludedDirection, direction) < _minAngleToDestination) {
                return false;
            }
            Vector3 fleePosition = transform.position + direction * safeDistance; // “очка, куда нужно убегать

            NavMeshHit hit;
            if (NavMesh.SamplePosition(fleePosition, out hit, safeDistance, NavMesh.AllAreas) && (hit.position - transform.position).magnitude > safeDistance2 * Time.deltaTime) {
                position = hit.position;
                position.y = _visionPoint.position.y;
                if (Physics.Raycast(_visionPoint.position, position - _visionPoint.position, (_visionPoint.position - position).magnitude, _raycastMask)) {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool TryToRunAway(Vector3 enemyPosition) {
            var directions = new List<Vector3>() {
             Vector3.forward,
             Vector3.back,
             Vector3.right,
             Vector3.left,
             Vector3.forward + Vector3.right,
             Vector3.forward + Vector3.left,
             Vector3.back + Vector3.right,
             Vector3.back + Vector3.left,
            };
            var directionToPlayer = enemyPosition - transform.position;
            directionToPlayer.y = 0;
            var resultPositions = new List<Vector3>();
            foreach (var direction in directions) {
                TryToAddAvailablePoint(direction, directionToPlayer, resultPositions);
               /* if (TryToRunAway(direction, directionToPlayer, out var position)) {
                    resultPositions.Add(position);
                }*/
            }
            foreach (var position in resultPositions.OrderByDescending(value => (transform.position - value).sqrMagnitude)) {
                var path = new NavMeshPath();
                agent.CalculatePath(position, path);
                if (path.status != NavMeshPathStatus.PathComplete) {
                    continue;
                }
                agent.isStopped = false;
                agent.SetDestination(position);
                return true;
            }
            agent.isStopped = true;
            return false;
        }

        private bool TryToAddAvailablePoint(Vector3 direction, Vector3 excludedDirection, List<Vector3> result) {
            excludedDirection.y = 0;
            if (Vector3.Angle(excludedDirection, direction) < _minAngleToDestination) {
                return false;
            }
            Physics.Raycast(_visionPoint.position, direction, out var hitInfo, _searchPointDistance, _raycastMask);
            Debug.DrawLine(_visionPoint.position, hitInfo.point);
            result.Add(WorldToGrid(hitInfo.point));
            return true;
        }
    }
}
