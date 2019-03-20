using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is called Level Manager but I guess this is one single state in the big FSM picture, so it will be called ActionMode transition or something along the lines

public class LevelManager : StateMachineBehaviour
{
   public enum Phases
    {
        PlayerPhase,
        EnemyPhase,
        PausePhase,
        Window, //some random window that can stop some parts of the game so the player can read it( tutorial UI or character dialogue)
    }

    [SerializeField]
    private int commmandPoints;

    //public CameraBehaviour camBehaviour;
    //public NewPerspectiveSwitcher camBehaviour;
   //public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
   //{
   //    camBehaviour.orthoOn = false;
   //}
    

    public override void OnStateIK(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }

}
