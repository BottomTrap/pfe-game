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

            public void FixedTick()
            {
                ExecuteActionsArray(fixedActions);
            }

            public void Tick()
            {
                ExecuteActionsArray(updateActions);
                forceSkip = false;
            }

            void ExecuteActionsArray(StateAction[] list)
            {
                if (list == null)
                    return;

                for (int i = 0; i < list.Length; i++)
                {
                    if (forceSkip)
                        break;

                    forceSkip = list[i].Execute();
                }
            }
        }

}
