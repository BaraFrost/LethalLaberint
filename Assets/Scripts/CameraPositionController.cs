using UnityEngine;

namespace Game
{

    public class CameraPositionController : MonoBehaviour
    {

        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private float _smooth;

        private Vector3 _velocity = Vector3.zero;

        private Vector3 _cameraPlayerPositionDifference;

        private float yPosition;

        void Start()
        {   
            _cameraPlayerPositionDifference = _player.gameObject.transform.position - gameObject.transform.position;
        }

        void Update()
        {
            transform.position = _player.gameObject.transform.position - _cameraPlayerPositionDifference;//Vector3.SmoothDamp(transform.position, _player.gameObject.transform.position - _cameraPlayerPositionDifference, ref _velocity, _smooth);
        }
    }
}