using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NewMatrixBlender))]
public class NewPerspectiveSwitcher : MonoBehaviour
{
    private Matrix4x4 ortho,
                        perspective;
    public float fov = 60f,
                        near = .3f,
                        far = 1000f,
                        orthographicSize = 50f;
    private float aspect;
    private NewMatrixBlender blender;
    private bool orthoOn;
    private bool isDraggig = false;
    Camera m_camera;


    public GameObject player;       //Public variable to store a reference to the player game object


    [SerializeField] private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private Vector3 orthoOffset;
    float horizontal = 0.0f;
    void Start()
    {
        //Third Person Camera Stuff
        offset = new Vector3(-2, 1, -4);
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
        orthoOffset = transform.position - player.transform.position;
        
        
        //Perspective switcher stuff

        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        m_camera = GetComponent<Camera>();
        m_camera.projectionMatrix = ortho;
        orthoOn = true;
        blender = (NewMatrixBlender)GetComponent(typeof(NewMatrixBlender));
    }

    void Update()
    {
        orthoOffset = transform.position - player.transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            orthoOn = !orthoOn;
            if (orthoOn)
            {       blender.BlendToMatrix(ortho, 1f, 8, true);
            transform.position = orthoOffset;
            }
            else
            blender.BlendToMatrix(perspective, 1f, 8, false);
                                
        }
     
    }
    void LateUpdate()
    {
        if (!orthoOn)
        {
            CameraMovement();

        }
    }
    private void OnMouseDrag()
    {
        isDraggig = true;
        horizontal = Input.GetAxis("Mouse X") * rotateSpeed;

    }
    private void OnMouseUp()
    {
        isDraggig = false;
    }

    void CameraMovement()
    {
        float angle = player.transform.eulerAngles.y;
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;
        if (isDraggig)
        {
            //horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            transform.RotateAround(player.transform.position, Vector3.up, angle * horizontal);
        }
        else
        {
            horizontal = 0; //reset horizontal value
        }
        

        
        
        
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = Vector3.Lerp(transform.position,player.transform.position +  offset, rotateSpeed*Time.deltaTime);
        //transform.LookAt(player.transform);
        Vector3 direction = player.transform.position - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
    }
}