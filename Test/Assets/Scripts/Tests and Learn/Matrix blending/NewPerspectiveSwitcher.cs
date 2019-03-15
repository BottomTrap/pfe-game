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
    Camera m_camera;


    private Transform playerTransform;       //Public variable to store a reference to the player transform from ObjectClick.cs when delegate is activated
    private bool transitionning=false;

    [SerializeField] private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    [SerializeField] private float rotateSpeed = 5f;
    private Transform oldTransform;
    //float horizontal = 0.0f;
    void Start()
    {
        //Delegate subscription 
        ObjectClick.characterSelectDelegate += CamTrans;
        //Third Person Camera Stuff
        offset = new Vector3(-2, 1, -4);
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
        
        
        
        //Perspective switcher stuff

        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        m_camera = GetComponent<Camera>();
        m_camera.projectionMatrix = ortho;
        orthoOn = true;
        blender = (NewMatrixBlender)GetComponent(typeof(NewMatrixBlender));
        blender.BlendToMatrix(ortho, 1f, 8, true);

        // 
        orthoOn = true;
    }

    void Update()
    {

        //orthoOn = !orthoOn;
        if (orthoOn)
            blender.BlendToMatrix(ortho, 1f, 8, true);
        else if( !orthoOn && transitionning)
        {
            
            CameraTransition();
        }
        if (!orthoOn && !transitionning)
        {
            CameraMovement();
        }
        if (orthoOn && !transitionning)
        {
            IsoMovement();
        }
        
           

    }
  //void LateUpdate() //NOTE TO SELF : LATE UPDATE DOES WEIRD THINGS WHEN ITS WITH UPDATE SIMULATNEOUSLY 
  //{
  //    if (!orthoOn)
  //    {
  //        CameraMovement();
  //    }
  //}
    void CamTrans()
    {
        transitionning = true;
        orthoOn = false;
        Debug.Log(orthoOn);
        Debug.Log(transitionning);
        blender.BlendToMatrix(perspective, 1f, 8, false);



    }
    void CameraTransition() // Cool looking lerp
    {
        oldTransform = transform;
        playerTransform = ObjectClick.objectPos;
        float angle = playerTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Vector3 firstLerp = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, firstLerp, rotateSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, rotateSpeed * Time.deltaTime);
        //transform.LookAt(player.transform);
        Vector3 direction = playerTransform.position - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        transitionning = false;
    }

    void IsoCameraTransition()
    {
        transform.position = Vector3.Lerp(transform.position, oldTransform.position, rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, oldTransform.rotation, rotateSpeed * Time.deltaTime);
    }
    void CameraMovement() //player follow !! to make after we made camera transition
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = playerTransform.position + offset;
       // if (Input.GetMouseButton(1))
       // {
       //     horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
       // }
       // player.transform.Rotate(0, horizontal, 0);
        float angle = playerTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = playerTransform.position + rotation * offset;
        transform.LookAt(playerTransform);


    }
    void IsoMovement()
    {
        //Keyboard Scroll

        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");
        float fastTranslationX = 2 * Input.GetAxis("Horizontal");
        float fastTranslationY = 2 * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(fastTranslationX , 0, fastTranslationY);
        }
        else
        {
            transform.Translate(translationX ,0 , translationY, Space.World );
        }

        //Mouse Scroll

        var mousePosX = Input.mousePosition.x;
        var mousePosY = Input.mousePosition.y;
        int scrollDistance = 3;
        float scrollSpeed = 15f;

        //Horizontal Camera Movement
        if (Input.GetKey(KeyCode.Space))
        {
            if (mousePosX < scrollDistance)
            {
                //horizontal left
                transform.Translate(-1, 0, 1);
            }
            if (mousePosY >= Screen.width - scrollDistance)
            {
                //horizontal right
                transform.Translate(1, 0, -1);
            }

            //Vertical Camera Movement
            if (mousePosY < scrollDistance)
            {
                //scrolling down
                transform.Translate(-1, 0, -1);
            }
            if (mousePosY >= Screen.height - scrollDistance)
            {
                //scrolling up
                transform.Translate(1, 0, 1);
            }
        }
        if (Input.GetMouseButton(1))
        {
            translationX = Input.GetAxis("Mouse X");
            transform.Rotate(axis: new Vector3(0,1,0), angle: translationX * scrollSpeed * Time.deltaTime,Space.World);
        }
    }
}