using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 maxSpeed;
    public float acceleration = 1000f; // Vitesse de déplacement du joueur

    public float lerpingMovement;

    public float jumpForce = 10f; // Force de saut du joueur
    public Transform groundCheck; // Référence au GameObject qui vérifie si le joueur touche le sol
    public LayerMask groundMask; // Masque de la couche représentant le sol

    Vector2 inputMovement;
    Vector3 moveDirection;

    public float decreaseMovement = 2;
    
    
    private Rigidbody rb;
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

        // Vérifie si le joueur touche le sol
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        // Déplacement horizontal
        float horizontalInput = inputMovement.x;
        float verticalInput = inputMovement.y;
        moveDirection = Vector3.Lerp(moveDirection,  new Vector3(horizontalInput, 0f, verticalInput).normalized, lerpingMovement*dt);
        
        

        // Applique la force de déplacement seulement si le joueur touche le sol
        if (isGrounded)
        {
            if (inputMovement.magnitude > 0)
            {
                rb.AddForce((moveDirection * acceleration) * dt);
                float clampX = Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x);
                float clampY = Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y);
                float clampZ = Mathf.Clamp(rb.velocity.z, -maxSpeed.z, maxSpeed.z);
                
                rb.velocity = new Vector3(clampX, clampY, clampZ);
            }
            else
            {
                rb.velocity = Vector3.Lerp(rb.velocity,Vector3.zero,decreaseMovement*dt);
            }
            

            transform.up = Vector3.up;

            transform.forward = moveDirection.normalized;
        }
    }

    private void OnInputMove(Vector2 input)
    {
        inputMovement = input;
    }
}
