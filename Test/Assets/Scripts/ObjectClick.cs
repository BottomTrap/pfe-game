using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectClick : MonoBehaviour 
{
    private bool _mouseover = false;
    public delegate void OnCharacterSelectDelegate();
    public static event OnCharacterSelectDelegate characterSelectDelegate;
    public static Transform objectPos;

  
    public void OnMouseDown()
    {
        Debug.Log("GameObject Clicked");
        objectPos = transform;
        if (tag=="Player")
        characterSelectDelegate();
        else if (tag == "Enemy")//do things if the clicked object is an enemy
        {

        }
        
    }

    private void OnGUI()
    {
        if (!_mouseover) return;
        GUI.TextField(new Rect(Input.mousePosition.x,Input.mousePosition.y, 100, 30), "This is player");
    }

    private void OnMouseOver()
    {
        _mouseover = true;
    }

    private void OnMouseExit()
    {
        _mouseover = false;
    }

}
