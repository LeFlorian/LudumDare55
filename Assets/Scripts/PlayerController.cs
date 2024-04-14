using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 maxSpeed;
    public float acceleration = 1000f; // Vitesse de déplacement du joueur

    public float lerpingMovement;

    public Transform groundCheck; // Référence au GameObject qui vérifie si le joueur touche le sol

    Vector2 inputMovement;
    Vector3 moveDirection;

    public float decreaseMovement = 2;

    public float lerpDirection = 20;

    public float gravity = 9.81f;
    private float g;
    
    public Rigidbody rb;
    private bool isGrounded;


    private InputManager im;


    void Start()
    {
        im = InputManager.inst;
        im.move.AddListener(OnInputMove);


        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        // Déplacement horizontal
        float horizontalInput = inputMovement.x;
        float verticalInput = inputMovement.y;
        moveDirection = Vector3.Lerp(moveDirection,  new Vector3(horizontalInput, 0f, verticalInput).normalized, lerpingMovement*dt);

        TestGround();

        g = gravity;

        // Applique la force de déplacement seulement si le joueur touche le sol
        if (isGrounded)
        {
            if (inputMovement.magnitude > 0)
            {
                Vector3 gravityMotion = g * -Vector3.up;

                rb.AddForce((moveDirection * acceleration + gravityMotion) * dt);
                float clampX = Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x);
                float clampY = Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y);
                float clampZ = Mathf.Clamp(rb.velocity.z, -maxSpeed.z, maxSpeed.z);
                
                rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(clampX, clampY, clampZ), decreaseMovement * dt);
            }
            else
            {

                rb.velocity = Vector3.Lerp(rb.velocity,Vector3.zero,decreaseMovement*dt);
            }
            
        }
        else
        {

            rb.velocity += -Vector3.up * g *dt;

        }



        transform.up = Vector3.up;

        transform.forward = Vector3.Lerp(transform.forward, moveDirection.normalized, lerpDirection*dt);
    }

    private void TestGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,-Vector3.up, out hit))
        {
            float dist = Vector3.Distance(hit.point, transform.position);

            if (dist < 1.1f)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
    }

    private void OnInputMove(Vector2 input)
    {
        inputMovement = input;
    }
}
