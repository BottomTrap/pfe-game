using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG
{
        public class State
        {
            private StateAction[] updateActions;
            private StateAction[] fixedActions;

            private bool forceSkip;


            public State(StateAction[] updateActions, StateAction[] fixedUpdateActions)
            {
                this.updateActions = updateActions;
                this.fixedActions = fixedUpdateActions;
            }
        }

}
