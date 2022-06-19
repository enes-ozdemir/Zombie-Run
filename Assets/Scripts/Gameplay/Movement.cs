using UnityEngine;
public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.forward, movementSpeed * Time.deltaTime);
        _rigidbody.AddForce(new Vector3(0, 0, movementSpeed * Time.deltaTime));
    }
}