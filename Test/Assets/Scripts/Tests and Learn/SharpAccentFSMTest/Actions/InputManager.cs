using UnityEngine;
using System.Collections;


namespace RG
{
    public class InputManager : StateAction
    {
			PlayerStateManager states;
			string targetState;

			public InputManager(PlayerStateManager playerStateManager,string targetState)
			{
				states = playerStateManager;
				this.targetState = targetState;
			}

			public override bool Execute()
			{

				states.horizontal = Input.GetAxis("Horizontal");
				states.vertical = Input.GetAxis("Vertical");

				Debug.Log("this is in locomotion state");

				if (Input.GetKeyDown(KeyCode.Space))
				{
					states.SetState(targetState);
					return true;
				}

				return false;
			}
			
    }
}