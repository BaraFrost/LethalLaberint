using NaughtyAttributes;
using System.Collections;
using UnityEngine;

namespace Game {

    public class LabyrinthCellWithDoor : LabyrinthCell {

        [SerializeField]
        private GameObject _door;

        [SerializeField]
        private Transform _closePosition;
        [SerializeField]
        private Transform _openPosition;

        [SerializeField]
        private float _openingTime;

        private bool _isOpen;

        private bool _inCoroutine;

        [Button]
        public void ChangeDoorState() {
            if (_inCoroutine) {
                return;
            }
            StartCoroutine(ChangeStateCoroutine());
        }

        public IEnumerator ChangeStateCoroutine() {
            _inCoroutine = true;
            var target = _isOpen ? _closePosition : _openPosition;
            var distance = Vector3.Distance(target.transform.position, _door.transform.position);
            var time = _openingTime;
            while (time >= 0) {
                time -= Time.deltaTime;
                _door.transform.position = Vector3.MoveTowards(_door.transform.position, target.position, distance / (_openingTime / Time.deltaTime));
                yield return null;
            }
            _door.transform.position = target.position;
           _isOpen = !_isOpen;
            _inCoroutine = false;
        }
    }
}

