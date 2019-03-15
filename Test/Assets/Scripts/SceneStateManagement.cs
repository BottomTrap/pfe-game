using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneStateManagement : MonoBehaviour
{
    public enum SceneState
    {
        Menu = 100,
        TacticsMode = 200,
        ActionMode = 300,
    }
    [SerializeField]
    private SceneState currentState;

    private void Start()
    {
        
        //currentState = SceneState.Menu;
        //SceneManager.LoadScene("MainMenu");
    }

    //void ChangeState()
    //{
    //    if (currentState == SceneState.ActionMode || currentState == SceneState.Menu)
    //        currentState = SceneState.TacticsMode;
    //    else
    //    {
    //        currentState = SceneState.ActionMode;
    //    }
    //    Debug.Log(currentState);
    //}


    private void Update()
    {
           }
}
