using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
    

    [SerializeField] private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    [SerializeField] private float rotateSpeed=5f;
    float horizontal = 0.0f;
    // Use this for initialization
    void Start()
    {
        //offset = new Vector3(-2, 1, -4);
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void FixedUpdate()
    {

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;
        if (Input.GetMouseButton(1))
        {
             horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        }
        player.transform.Rotate(0, horizontal, 0);
        float angle = player.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = player.transform.position + rotation* offset;
        transform.LookAt(player.transform);
    }
}
