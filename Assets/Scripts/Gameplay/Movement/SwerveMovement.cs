using UnityEngine;

namespace Gameplay
{
    public class SwerveMovement : MonoBehaviour
    {
        private SwerveInputSystem _swerveInputSystem;
        [SerializeField] private float swerveSpeed = 0.5f;
        [SerializeField] private float maxSwerveAmount = 1f;

        private void Awake()
        {
            _swerveInputSystem = GetComponent<SwerveInputSystem>();
        }

        private void FixedUpdate()
        {
            float swerveAmount = _swerveInputSystem.MoveFactorX * swerveSpeed * Time.deltaTime;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
            transform.Translate(swerveAmount, 0, 0);
        }
    }
}