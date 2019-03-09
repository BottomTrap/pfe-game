using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectClick.characterSelectDelegate += CameraTransition;
    }
    
    void IsometricCam()
    {
        //Keyboard Scroll

        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");
        float fastTranslationX = 2 * Input.GetAxis("Horizontal");
        float fastTranslationY = 2 * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(fastTranslationX + fastTranslationY, 0, fastTranslationY - fastTranslationX);
        }
        else
        {
            transform.Translate(translationX + translationY, 0, translationY - translationX);
        }

        //Mouse Scroll

        var mousePosX = Input.mousePosition.x;
        var mousePosY = Input.mousePosition.y;
        int scrollDistance = 3;
        float scrollSpeed = 4f;

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
        //Zoom
        Camera Eye = GetComponentInChildren<Camera>();

        // 
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Eye.orthographicSize > 4)
        {
            Eye.orthographicSize = Eye.orthographicSize - scrollSpeed*Time.deltaTime;
        }

        // 
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Eye.orthographicSize < 80)
        {
            Eye.orthographicSize = Eye.orthographicSize + scrollSpeed * Time.deltaTime ;
        }

        //default zoom
        //if (Input.GetKeyDown(KeyCode.Mouse2))
        //{
        //    Eye.orthographicSize = 50;
        //}

        //Rotation
        if (Input.GetMouseButton(1))
        {
            translationX = Input.GetAxis("Mouse X");
            transform.Rotate(axis: Vector3.up, angle: translationX * scrollSpeed * Time.deltaTime);
        }
    }

    void CameraTransition()
    {
        //Transform objPos = ObjectClick.objectPos;
        while (transform.position != ObjectClick.objectPos.position)
        this.transform.position = Vector3.Lerp(transform.position, ObjectClick.objectPos.position, Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {

        IsometricCam(); // Activate Isometric Camera

        
    }
}
