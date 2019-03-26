using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using SA;

//THIS IS THE HUGE STATE MANAGER THAT WILL DEAL WITH THE WHOLE LEVEL
//UPDATE on the upper comment : this could be put on all object that will use this, similar to ObjectClick or PlayerStats

namespace SA
{
    public class StateManager : MonoBehaviour
    {
        //public float health;
        
        public State currentState;

        public SA.StateManagers.PlayerStateManager playerMan;
        public NewPerspectiveSwitcher cameraScript;


        //public GameObject objRef;
        public float delta;
     
        //public Transform mTransform;

        private void Start()
        {
            //objRef = this.gameObject;
            //  mTransform = this.transform
        }


        //public void ChangePerspective()
        //{
        //    if (cameraScript.orthoOn)
        //    {
        //        Debug.Log("Perspective View");
        //    }
        //    else
        //    {
        //        Debug.Log("OrthoView");
        //    }
        //}
        //public void SetMovementOn()
        //{
        //    Debug.Log("Space pressed");
        //    playerMovement.enabled = true;
        //}

        private void Update()
        {
            if(currentState != null)
            {
                currentState.Tick(this);
            }
        }
    }
}
