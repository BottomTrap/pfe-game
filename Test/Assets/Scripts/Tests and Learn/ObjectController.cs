using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    Renderer objRendrer;
    bool color = true;

    // Start is called before the first frame update
    void Start()
    {
        DelegateHandler.buttonClickDelegate += ChangePosition;
        DelegateHandler.buttonClickDelegate += ChangeColor;
        //DelegateHandler.buttonClickDelegate = ChangeRotation;
        objRendrer = GetComponent<Renderer>();
    }
    void ChangePosition()
    {
        transform.position += Vector3.up ;
    }
    void ChangeColor()
    {
        if (color)
        {
            objRendrer.material.color = Color.yellow;
            color = false;
        }else
        {
            objRendrer.material.color = Color.red;
            color = true;
        }
    }
    void ChangeRotation()
    {
        transform.rotation = Quaternion.Euler(Vector3.up);
    }

    private void OnDisable()
    {
        DelegateHandler.buttonClickDelegate -= ChangeColor;
        DelegateHandler.buttonClickDelegate -= ChangePosition;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
