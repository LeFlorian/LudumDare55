using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement du joueur

    public float lerpingMovement;

    public float jumpForce = 10f; // Force de saut du joueur
    public Transform groundCheck; // Référence au GameObject qui vérifie si le joueur touche le sol
    public LayerMask groundMask; // Masque de la couche représentant le sol

    Vector2 inputMovement;
    Vector3 moveDirection;

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
            rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * speed * dt);
        }

        transform.up = Vector3.up;

    }

    private void OnInputMove(Vector2 input)
    {
        inputMovement = input;
    }
}
