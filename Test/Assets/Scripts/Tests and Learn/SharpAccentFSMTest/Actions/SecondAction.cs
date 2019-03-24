using UnityEngine;
using System.Collections;


namespace RG
{
    public class SecondAction: StateAction
    {
			public override bool Execute()
			{
				Debug.Log("this is second action");

				return false;
			}
    }
}