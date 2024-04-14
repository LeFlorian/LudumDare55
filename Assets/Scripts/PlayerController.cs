using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 maxSpeed;
    public float acceleration = 1000f; // Vitesse de déplacement du joueur

    public float lerpingMovement;

    Vector2 inputMovement;
    Vector3 moveDirection;

    public float decreaseMovement = 2;

    public float lerpDirection = 20;

    public float gravity = 9.81f;
    private float g;
    
    public Rigidbody rb;
    private bool isGrounded;

    public Animator animator;

    private InputManager im;

    private EventInstance playerFootsteps;

    void Start()
    {
        im = InputManager.inst;
        im.move.AddListener(OnInputMove);


        rb = GetComponent<Rigidbody>();

        playerFootsteps = AudioManager.instance.createInstance(FMODEvents.instance.playerFootsteps);
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        TestGround();

        // Déplacement horizontal
        float horizontalInput = inputMovement.x;
        float verticalInput = inputMovement.y;
        moveDirection = Vector3.Lerp(moveDirection,  new Vector3(horizontalInput, 0f, verticalInput).normalized, lerpingMovement*dt);

        // Applique la force de déplacement seulement si le joueur touche le sol
        if (isGrounded)
        {

            g = gravity;

            Vector3 gravityMotion = -Vector3.up * 1f;


            if (inputMovement.magnitude != 0)
            {
                animator.SetBool("Walk",true);

                rb.AddForce((moveDirection * acceleration + gravityMotion) * dt);
                float clampX = Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x);
                float clampY = Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y);
                float clampZ = Mathf.Clamp(rb.velocity.z, -maxSpeed.z, maxSpeed.z);

                rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(clampX, clampY, clampZ), decreaseMovement * dt);
                UpdateSound();
            }
            else
            {
                animator.SetBool("Walk", false);
                rb.velocity = Vector3.Lerp(rb.velocity, gravityMotion, dt * decreaseMovement);

                UpdateSound();
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            g += gravity;
            rb.AddForce(-Vector3.up * g * dt,ForceMode.VelocityChange);
            UpdateSound();
        }

        transform.up = Vector3.Lerp(transform.up, Vector3.up, lerpDirection * dt);

        if (moveDirection != Vector3.zero)
            transform.forward = Vector3.Lerp(transform.forward, moveDirection.normalized, lerpDirection*dt);

        UpdateSound();
    }

    private void TestGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position+Vector3.up,-Vector3.up, out hit))
        {
            float dist = Vector3.Distance(hit.point, transform.position+Vector3.up);

            if (dist <= 1.1f)
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

    private void UpdateSound()
    {
        if (rb.velocity.x != 0 && isGrounded)
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
