using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

[CreateAssetMenu (menuName = "Conditions/Test/Push Space")]
public class SpaceInputScript : Condition
{
    
    public override bool CheckCondition(StateManager state)
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
