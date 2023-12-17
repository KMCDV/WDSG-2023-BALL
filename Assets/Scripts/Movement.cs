using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float acceleration = 3f;
    public float maxSpeed = 10f;
    public float jumpForce = 6f;
    public float dashForce = 10f;

    public int maxJumps = 2;
    private int _currentJumps = 0;
    
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        float cameraAngleY = Camera.main.transform.eulerAngles.y;
        
        Vector3 direction = new(inputX, 0, inputY);

        rb.velocity += Quaternion.Euler(0, cameraAngleY, 0) * direction * (acceleration * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && _currentJumps < maxJumps)
        {
            rb.velocity += Vector3.up * jumpForce;
            _currentJumps++;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 dashDirection = Camera.main.transform.forward;
            dashDirection.y = 0;
            rb.velocity += dashDirection * dashForce;
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        if (transform.position.y < -5f) SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _currentJumps = 0;
        }
    }
}
