using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Camera playercam;

    Rigidbody rb;
    Ray ray;
    RaycastHit hit;
    float verticalMove;
    float horizontalMove;

    public float speed = 5f;
    public float jumpHeight = 10f;
    public float groundDetectLength;

    public void Start()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody>();
        playercam = Camera.main;

       // Cursor.visible = false;
       // Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
    
        // Camera Rotation System

        Quaternion playerRotation = playercam.transform.rotation;
        playerRotation.x = 0;
        playerRotation.z = 0;
        transform.rotation = playerRotation;

        // Movement System
        Vector3 temp = rb.linearVelocity;

        temp.x = verticalMove * speed;
        temp.z = horizontalMove * speed;


        ray.origin = transform.position;
        ray.direction = -transform.up;

 

        rb.linearVelocity = (temp.x * transform.forward) +
                            (temp.y * transform.up) +
                            (temp.z * transform.right);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputAxis = context.ReadValue<Vector2>();

        verticalMove = inputAxis.y;
        horizontalMove = inputAxis.x;
    }
    public void Jump()
    {
        if (Physics.Raycast(ray, out hit, groundDetectLength))
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);


    }
}
