using UnityEngine;

namespace MiniGames.FlappyBird {

    public class FlappyBirdObstacle : MonoBehaviour {

        public float speed = 2f;

        [SerializeField]
        private bool _needMove;

        [SerializeField]
        private bool _needRotate;

        private void OnEnable() {
            if(_needRotate) {
                transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            }
        }

        public void UpdateObstacle(float speedModifier) {
            if(_needMove) {
                transform.position += Vector3.left * speed * speedModifier * Time.deltaTime;
            }
        }
    }
}