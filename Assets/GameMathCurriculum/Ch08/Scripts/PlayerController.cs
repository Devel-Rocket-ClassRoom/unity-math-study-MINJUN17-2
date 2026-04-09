using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 8f;
    public float rotationSpeed = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.forward * z + transform.right * x).normalized;

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);
    }
}
