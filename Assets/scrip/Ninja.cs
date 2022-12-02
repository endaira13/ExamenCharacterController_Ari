using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    public float speed = 5f;
    public float JumpHeight = 3f;
    private CharacterController controller;
    private float currentVelocity;
    private Transform cam;
    private Vector3 playerVelocity;
    private float gravity = -13f;
    [SerializeField]private Transform groundSensor;
    public float sensorRadius;
    public LayerMask groundLayer;
    [SerializeField]private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
        if(movement!= Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDirection = Quaternion. Euler(0, targetAngle, 0) * Vector3.forward;
            controller. Move(moveDirection * speed * Time.deltaTime);
        }

        

        isGrounded = Physics.CheckSphere(groundSensor.position, sensorRadius, groundLayer);

        if(playerVelocity.y < 0 && isGrounded)
        {
            playerVelocity.y = 0;
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(JumpHeight * -2 * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
