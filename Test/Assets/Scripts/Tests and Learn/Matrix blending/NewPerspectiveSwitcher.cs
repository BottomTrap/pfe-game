using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NewMatrixBlender))]
public class NewPerspectiveSwitcher : MonoBehaviour
{
    //Isometric and Perspective Switch stuff here
    private Camera m_camera;

    private Matrix4x4 ortho,
                        perspective;
    public float fov = 60f,
                        near = .3f,
                        far = 1000f,
                        orthographicSize = 50f;
    private float aspect;
    private NewMatrixBlender blender;
    public bool orthoOn = true;    



    //Player Movement activation
    private PlayerMovement playerMovement;
    private Transform playerTransform;       //where to store the reference to the player transform from ObjectClick.cs
    private GameObject player;



    //Camera translation and rotattion stuff for the AUTO transition between Modes
    public Vector3 offset;         //Private variable to store the offset distance between the player and camera
    [SerializeField] private float rotateSpeed = 5f;
    private Transform orthoOffset;
    float horizontal = 0.0f;
    private bool isDraggig = false;
    void Start()
    {
        //Object Click to get the reference to the clicked object to do the camera transition towards it
        ObjectClick.characterSelectDelegate += CameraTransition;

        //Original Offset for the ortho view to return to
        orthoOffset = transform;

        //Third Person Camera Stuff , offset for the third person camera view
        offset = new Vector3(-1, 3, -4);


        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
        //orthoOffset = transform.position - player.transform.position;
        
        
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
        if (orthoOn) //add the condition to return to ortho (player unit finishes turn) TO DO!!!!!
        {
            
            blender.BlendToMatrix(ortho, 1f, 8, true);


        }
        if (!orthoOn) 
        {
            blender.BlendToMatrix(perspective, 1f, 8, false);
        }
    }
    void LateUpdate()
    {
        orthoOffset = transform;
        if (player != null)
        
            
            {
                CameraMovement(player);
                
                playerMovement.enabled = true;
            }
        
        else
        {
            playerMovement.enabled = false;
            this.transform.position = Vector3.Lerp(transform.position, orthoOffset.position, rotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, orthoOffset.rotation, rotateSpeed * Time.deltaTime);
            player = null;
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

    void CameraMovement(GameObject player)
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
    void CameraTransition()
    {
        //playerTransform = ObjectClick.objectPos;
        //playerMovement = playerTransform.GetComponent<PlayerMovement>();
        player = ObjectClick.objectRef;
        playerMovement = player.GetComponent<PlayerMovement>();
        orthoOn = false;
    }
}