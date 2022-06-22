using UnityEngine;

namespace Gameplay
{
    public class Waypoint : MonoBehaviour
    {
        public Operation operation;
        public int value;
        public bool canFight;
        public Type waypointType;
        public GameObject gatePrefab;

        public enum Operation
        {
            Add,
            Subtract,
            Multiply,
            Divide
        }

        public enum Type
        {
            People,
            Police,
            Boss
        }
    }
}