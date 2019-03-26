using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.StateManagers
{
    [RequireComponent(typeof(StateManager))]
    public class PlayerStateManager : MonoBehaviour
    {
        public StateManager states;
        // Start is called before the first frame update
        void Start()
        {
            states = GetComponent<StateManager>();
            states.playerMovement = this;

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
