using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PerspectiveSwitcher : MonoBehaviour
{
    private Matrix4x4 ortho,
                        perspective;
    public float fov = 60f,
                        near = .3f,
                        far = 1000f,
                        orthographicSize = 50f,
                        angle;
    private float aspect;
    private MatrixBlender blender;
    private bool orthoOn;
    public Transform target;
    private Vector3 point;
    public Vector3 offset;

    private void Start()
    {
        point = target.position;
        offset = transform.position - target.position;
        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize,orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        Camera.main.projectionMatrix = ortho;
        orthoOn = true;
        blender = (MatrixBlender)GetComponent(typeof(MatrixBlender));
    }

    private void Update()
    {
        angle = target.eulerAngles.y;
        
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            orthoOn = !orthoOn;
            if (orthoOn)
                blender.BlendToMatrix(ortho, point, offset, angle, 1f);
            else
                blender.BlendToMatrix(perspective, point, offset, angle, 1f);
                
        }
    }

}
