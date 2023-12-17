using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField, Header("Player Preset")] private PlayerMovementPreset playerMovementPreset;
    
    private Camera mainCamera;
    private int _currentJumps = 0;
    
    [SerializeField, Header("Keybinds")] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
        playerMovementPreset.ApplyMassToRigidbody(gameObject);
        mainCamera = Camera.main;
        if(mainCamera == null)
            Debug.LogError("Main camera is not found!");
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        float cameraAngleY = mainCamera.transform.eulerAngles.y;
        
        Vector3 direction = new(inputX, 0, inputY);

        rb.velocity += Quaternion.Euler(0, cameraAngleY, 0) * direction * (playerMovementPreset.Acceleration * Time.deltaTime);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, playerMovementPreset.MaxSpeed);

        
        if (Input.GetKeyDown(jumpKey) && _currentJumps < playerMovementPreset.MaxJumps)
        {
            rb.AddForce(Vector3.up * playerMovementPreset.JumpForce, ForceMode.Impulse);
            _currentJumps++;
        }

        if (Input.GetKeyDown(dashKey))
        {
            Vector3 dashDirection = mainCamera.transform.forward;
            dashDirection.y = 0;
            rb.AddForce(dashDirection * (playerMovementPreset.DashForce * 10f), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _currentJumps = 0;
        }
    }
    
    public void ResetPosition()
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = startingPosition;
        rb.isKinematic = false;
    }
}
