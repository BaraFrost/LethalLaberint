using NaughtyAttributes;
using System;
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

        [SerializeField]
        private MaterialReplacer _materialReplacer;

        [SerializeField]
        private AudioSource _openCloseSound;

        public bool disableDoor;
        private bool _isOpen;
        private bool _inCoroutine;
        private bool _selected;

        public Action onDoorStateChanged;

        [Button]
        public void ChangeDoorState() {
            if(disableDoor) {
                return;
            }
            if (_inCoroutine) {
                return;
            }
            _openCloseSound.Play();
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
            onDoorStateChanged?.Invoke();
            _inCoroutine = false;
        }

        public void SelectDoor() {
            if(_selected) {
                return;
            }
            _selected = true;
            _materialReplacer.Switch();
        }

        public void DeselectDoor() {
            if (!_selected) {
                return;
            }
            _selected = false;
            _materialReplacer.Switch();
        }
    }
}

