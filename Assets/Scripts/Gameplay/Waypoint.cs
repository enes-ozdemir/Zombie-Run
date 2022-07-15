using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Transform leftWaypoint;
        [SerializeField] private Transform rightWaypoint;
        public Operation leftOperation;
        public Operation rightOperation;
        public int leftValue;
        public int rightValue;
        [SerializeField] private TextMeshPro leftValueText;
        [SerializeField] private TextMeshPro rightValueText;
        public bool isActive = false;

        public enum Operation
        {
            Add = 0,
            Subtract = 1,
            Multiply = 2,
            Divide = 3
        }

        private void Start()
        {
            InitWaypoint(leftWaypoint, leftValue, leftValueText, leftOperation);
            InitWaypoint(rightWaypoint, rightValue, rightValueText, rightOperation);
        }

        private void InitWaypoint(Transform waypoint, int value, TMP_Text valueText, Operation operation)
        {
            waypoint.GetChild((int) operation).gameObject.SetActive(true);

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