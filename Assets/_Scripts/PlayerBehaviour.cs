using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;

    [Header("Controls")]
    public Joystick joystick;
    public float horizontalSensitivity;
    public float verticalSensitivity;



    [Header("Control Properties")]
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Vector3 velocity;


    [Header("Ground Detection Properties")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("MiniMap")]
    public GameObject miniMap;

    [Header("Player Sounds")]
    public AudioSource jumpSound;
    public AudioSource hitSound;

    [Header("HealthBar")]
    public HealthBarScreenSpaceController healthBar;

    [Header("Player Abilities")]
    [Range(0, 100)]
    public int health = 100;

    void Start()
    {

    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        // Input for webGL and Desktop
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");


        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    Jump();
        //}


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    ToggleMinimap();
        //}

    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        jumpSound.Play();
    }

    void ToggleMinimap()
    {
        miniMap.SetActive(!miniMap.activeInHierarchy);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        hitSound.Play();
        healthBar.TakeDamage(damage);

        if (health < 0)
        {
            health = 0;
        }
    }

    public void OnJumpButtonPressed()
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    public void OnMapButtonPressed()
    {
        ToggleMinimap();
    }
}
