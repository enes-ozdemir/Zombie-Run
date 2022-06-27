using UnityEngine;

namespace Gameplay
{
    public class SwerveMovement : MonoBehaviour
    {
        private SwerveInputSystem _swerveInputSystem;
        [SerializeField] private float swerveSpeed = 0.5f;
        [SerializeField] private float maxSwerveAmount = 1f;
        [SerializeField] private float rightBoundary = 11f;
        [SerializeField] private float leftBoundary = 1.5f;

        private void Awake()
        {
            _swerveInputSystem = GetComponent<SwerveInputSystem>();
        }

        private void FixedUpdate()
        {
            SlideMovement();
        }

        private void SlideMovement()
        {
            float swerveAmount = _swerveInputSystem.MoveFactorX * swerveSpeed * Time.deltaTime;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
            transform.Translate(swerveAmount, 0, 0);

            BoundaryCheck();
        }

        private void BoundaryCheck()
        {
            if (transform.position.x >= rightBoundary)
            {
                transform.position = new Vector3(rightBoundary, transform.position.y, transform.position.z);
            }
            else if (transform.position.x <= leftBoundary)
            {
                transform.position = new Vector3(leftBoundary, transform.position.y, transform.position.z);
            }
        }
    }
}