using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    

    [SerializeField]
    private float moveSpeed = 30;
    [SerializeField]
    private float turnSpeed = 5f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
      
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontal , 0, vertical);
        movement = transform.TransformDirection(movement);

        characterController.SimpleMove(movement * Time.deltaTime * moveSpeed);

        

        if (movement.magnitude > 0)
        {
            Quaternion newDirection = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
        }
    }
}