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
    public static bool objectClicked;
    private PlayerMovement playerMovement;


    //private Animator animator;
    //public IsometricState isometricState;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerMovement = (PlayerMovement)GetComponent(typeof(PlayerMovement));
        //FSM Stuff
        //isometricState = animator.GetBehaviour<IsometricState>();
        
    }

    public void OnMouseDown()
    {
        Debug.Log("GameObject Clicked");
        objectClicked = true;
        objectPos = transform;
        
        if (tag == "Player" )
        {
            characterSelectDelegate();
            playerMovement.didHit = false;
            //animator.SetBool("orthoOn", false);
        }
        else if (tag == "Enemy")//do things if the clicked object is an enemy
        {
            //like some UI or stuff
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
