using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMovement))]
public class ObjectClick : MonoBehaviour 

{
    private bool _mouseover = false;
    public delegate void OnCharacterSelectDelegate();
    public static event OnCharacterSelectDelegate characterSelectDelegate;
    public static GameObject objectRef;

  
    public void OnMouseDown()
    {
        Debug.Log("GameObject Clicked");
        objectRef = this.gameObject;
        characterSelectDelegate();
        
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
