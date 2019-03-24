using UnityEngine;
using System.Collections;

namespace RG
{
    public class TestAction : StateAction
    {
			PlayerStateManager states;
			string locomotionState;

			public TestAction(PlayerStateManager playerStateManager, string locomotionState)
			{
				states = playerStateManager;
				this.locomotionState = locomotionState;
			}

			public override bool Execute()
			{
				Debug.Log ("this is test state");

				if (Input.GetKeyDown(KeyCode.Space))
				{
					states.SetState(locomotionState);

				}
				return false;
			}
    }
}