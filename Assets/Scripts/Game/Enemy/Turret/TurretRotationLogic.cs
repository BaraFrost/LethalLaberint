using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game {

    public class TurretRotationLogic : AbstractEnemyLogic<TurretEnemy> {

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private float _pursuitSpeed;

        [SerializeField]
        private Transform _rotationRoot;

        [SerializeField]
        private float _reachedAngle;

        [SerializeField]
        private Transform[] _rotationTargets;

        private Queue<Transform> _rotationTargetsQueue;

        private void Start() {
            var randomRotation = Random.Range(0, 360);
            gameObject.transform.rotation = Quaternion.AngleAxis(randomRotation, Vector3.up);
            if (Random.Range(0, 2) == 1) {
                _rotationTargets = _rotationTargets.Reverse().ToArray();
            }
        }

        public bool RotationReached(Vector3 position) {
            var angle = Mathf.Abs(GetAngle(position));
            return angle <= _reachedAngle;
        }

        public void Rotate(Vector3 lookAtPosition, bool usePursuitSpeed = false) {
            if (Enemy.VisionLogic.TemporaryDisabled) {
                return;
            }
            if (RotationReached(lookAtPosition)) {
                return;
            }
            var currentRotation = _rotationRoot.transform.rotation.eulerAngles.y;
            var differenceAngle = GetAngle(lookAtPosition);
            var rotation = Mathf.Lerp(currentRotation, differenceAngle + currentRotation, usePursuitSpeed ? _pursuitSpeed : _rotationSpeed * Time.deltaTime);
            _rotationRoot.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.up);
        }

        private float GetAngle(Vector3 lookAtPosition) {
            var direction = lookAtPosition - _rotationRoot.transform.position;
            direction.y = 0;
            var differenceAngle = Vector3.SignedAngle(_rotationRoot.forward, direction, Vector3.up);
            return differenceAngle;
        }

        public Vector3 GetTargetRotation() {
            if (_rotationTargetsQueue == null) {
                _rotationTargetsQueue = new Queue<Transform>(_rotationTargets);
            }
            var target = _rotationTargetsQueue.Dequeue();
            _rotationTargetsQueue.Enqueue(target);
            return target.position;
        }
    }
}
