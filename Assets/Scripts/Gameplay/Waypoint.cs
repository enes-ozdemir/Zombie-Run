using System.Collections;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Waypoint : MonoBehaviour
    {
        public Operation operation;
        public int value;
        [SerializeField] private TextMeshPro valueText;
        public bool isActive = true;

        public enum Operation
        {
            Add = 0,
            Subtract = 1,
            Multiply = 2,
            Divide = 3
        }

        private void Start()
        {
            transform.GetChild((int) operation).gameObject.SetActive(true);

            switch (operation)
            {
                case Operation.Add:
                    valueText.text = "+" + value;
                    break;
                case Operation.Subtract:
                    valueText.text = "-" + value;
                    break;
                case Operation.Multiply:
                    valueText.text = "x" + value;
                    break;
                case Operation.Divide:
                    valueText.text = "÷" + value;
                    break;
            }

        }

        public void SetInactive()
        {
            StartCoroutine(SetInactiveCoroutine());
        }
        
        private IEnumerator SetInactiveCoroutine()
        {
            Debug.Log("Set Active false");
            isActive = false;
            yield return new WaitForSeconds(0.5f);
            isActive = true;
        }
        
    }
}