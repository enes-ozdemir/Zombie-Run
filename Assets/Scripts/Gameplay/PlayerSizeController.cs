using UnityEngine;

namespace Gameplay
{
    public class PlayerSizeController : BaseSizeController
    {
        // #region Singleton
        //
        // public static PlayerSizeController instance;
        //
        // private void Awake()
        // {
        //     instance = this;
        // }
        //
        // #endregion
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag("Waypoint"))
            {
                Debug.Log("Entered to a waypoint");

                var currentOperation = collision.GetComponent<Waypoint>().operation;
                var currentValue = collision.GetComponent<Waypoint>().value;

                SetNewCharacterSize(currentOperation, currentValue, "Zombie");
            }
        }

        private void SetNewCharacterSize(Waypoint.Operation currentOperation, int currentValue, string charTag)
        {
            switch (currentOperation)
            {
                case Waypoint.Operation.Add:
                    AddCharacter(currentValue, charTag);
                    break;
                case Waypoint.Operation.Subtract:
                    RemoveCharacter(currentValue);
                    currentCharacterSize -= currentValue;
                    break;
                case Waypoint.Operation.Multiply:
                    AddCharacter(currentCharacterSize * currentValue - currentCharacterSize, charTag);
                    break;
                case Waypoint.Operation.Divide:
                    RemoveCharacter(currentCharacterSize - currentCharacterSize / currentValue);
                    break;
            }
        }
    }
}