using Scripts.Level.Ball;
using UnityEngine;

namespace Scripts.Platform.PlatformTrigger
{
    public class PlatformTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ball))
            {
                ball.StopMoving();
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ball))
            {
                ball.KeepMoving();
            }
        }
    }
}