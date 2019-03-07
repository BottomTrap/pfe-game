using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateHandler : MonoBehaviour
{

    public delegate void OnButtonClickDelegate();
    public static event  OnButtonClickDelegate buttonClickDelegate;

    public void OnButtonClick()
    {
        buttonClickDelegate();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
