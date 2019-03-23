using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class IsometricState : StateMachineBehaviour
{
   public enum Phases
    {
        PlayerPhase,
        EnemyPhase,
        PausePhase,
        Window, //some random window that can stop some parts of the game so the player can read it( tutorial UI or character dialogue)
    }

    [SerializeField]
    public int commmandPoints=8;

    public NewPerspectiveSwitcher camBehaviour;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //isoCameraTransition()
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

        camBehaviour.IsoMovement();
    }
}
