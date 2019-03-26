using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

    [CreateAssetMenu (menuName = "Actions/Test/Set Movement" )]
    public class SetMovement : StateActions
    {
        public override void Execute(StateManager states)
        {
            Debug.Log("it reached the execute");
            
        }
    }

