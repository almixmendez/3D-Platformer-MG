using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpForce = 2f;

    [SerializeField] Rigidbody rb;

    [SerializeField] Animator animator;
    [SerializeField] Transform player;

    public static PlayerMovement instance;

    private bool isGrounded;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Movimiento del player.
        float forwardMovement = Input.GetAxis("Vertical") * playerSpeed;
        float horizontalMovement = Input.GetAxis("Horizontal") * playerSpeed;

        forwardMovement *= Time.deltaTime;
        horizontalMovement *= Time.deltaTime;

        transform.Translate(horizontalMovement, 0, forwardMovement);

        // Animaciones.
        animator.SetFloat("VelZ", Mathf.Abs(horizontalMovement) + Mathf.Abs(forwardMovement));
        animator.SetBool("Grounded", isGrounded);

        // Salto.
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
