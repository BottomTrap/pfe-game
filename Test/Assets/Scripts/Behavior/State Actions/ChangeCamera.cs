using System.Collections;
using System.Collections.Generic;
using SA;
using UnityEngine;
[CreateAssetMenu(menuName = "Actions/Test/Change Camera")]
public class ChangeCamera : StateActions
{
    public override void Execute(StateManager states)
    {
        states.ChangePerspective();
    }
}
