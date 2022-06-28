using System;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Waypoint : MonoBehaviour
    {
        public Operation operation;
        public int value;
        public TextMeshPro valueText;

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
    }
}