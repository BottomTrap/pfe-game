using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class MatrixBlender : MonoBehaviour
{
    public static Matrix4x4 MatrixLerp (Matrix4x4 from, Matrix4x4 to , float time)
    {
        Matrix4x4 ret = new Matrix4x4();
        for (int i=0; i< 16; i++)
        {
            ret[i] = Mathf.SmoothStep(from[i], to[i], time);
        }
        return ret;
    }

    private IEnumerator LerpFromTo(Matrix4x4 source, Matrix4x4 destination , float duration) // lerping from ortho to pers view (or the opposite)
    {
        float startTime = Time.time;
        while (Time.time -startTime < duration)
        {
            Camera.main.projectionMatrix = MatrixLerp(source, destination, (Time.time - startTime) / duration);
            yield return new WaitForEndOfFrame();
        }
        Camera.main.projectionMatrix = destination;
    }
    private IEnumerator LerpBehind (Matrix4x4 source, Vector3 point, Vector3 offset,float angle,Matrix4x4 destination, float duration) //lerping postiton and rot
    {
        float startTime = Time.time;
        while (Time.time -startTime < duration)
        {

            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, (Time.time - startTime) / duration);
            new WaitForSeconds(.1f);
            transform.position = Vector3.Lerp(transform.position, offset, (Time.time - startTime) / duration);
            transform.LookAt(point);
            yield return StartCoroutine(LerpFromTo(source, destination, duration));
        }

    }
    public Coroutine BlendToMatrix (Matrix4x4 targetMatrix,Vector3 point,Vector3 offset,float angle, float duration)
    {
        StopAllCoroutines();
        return 
        StartCoroutine(LerpBehind(Camera.main.projectionMatrix,point,offset,angle, targetMatrix, duration));

    }
}
