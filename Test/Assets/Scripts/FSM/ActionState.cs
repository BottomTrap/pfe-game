using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : StateMachineBehaviour
{
    public NewPerspectiveSwitcher camBehaviour;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        camBehaviour.CameraTransition();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        camBehaviour.CameraMovement();
    }
    
}
