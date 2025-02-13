using System.Linq;
using UnityEngine;

namespace Game {

    public class TurretVisualLogic : AbstractEnemyLogic<TurretEnemy> {

        [SerializeField]
        private LineRenderer _laserLineRenderer;

        [SerializeField]
        private Transform _laserStart;

        [SerializeField]
        private ParticleSystem[] _shootParticles;

        [SerializeField]
        private ParticleSystem[] _prepareShootParticles;

        [SerializeField]
        private LayerMask _laizerLayerMask;

        [SerializeField]
        private Gradient _laserSearchColor;

        [SerializeField]
        private Gradient _laserAttackColor;

        private Vector3[] _points = new Vector3[2];

        public override void UpdateLogic() {
            base.UpdateLogic();
            if(Enemy.VisionLogic.TemporaryDisabled) {
                _laserLineRenderer.enabled = false;
                return;
            }
            _laserLineRenderer.enabled = true;
            _points[0] = _laserStart.position;
            if (Physics.Raycast(_laserStart.position, _laserStart.forward, out var hit, Enemy.VisionLogic.Distance, _laizerLayerMask)) {
                _points[1] = hit.point;
            } else {
                _points[1] = _laserStart.position + _laserStart.forward * Enemy.VisionLogic.Distance;
            }
            _laserLineRenderer.SetPositions(_points);
        }

        public void PlayShootEffects() {
            foreach (var effect in _shootParticles) {
                effect.Play();
            }
        }

        public void PlayPrepareShootEffects() {
            foreach (var effect in _prepareShootParticles) {
                effect.Play();
            }
            _laserLineRenderer.colorGradient = _laserAttackColor;
        }

        public void StopShootEffects() {
            foreach (var effect in _shootParticles.Concat(_prepareShootParticles)) {
                effect.Stop();
            }
            _laserLineRenderer.colorGradient = _laserSearchColor;
        }
    }
}

