using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
   public enum SceneState {
        Menu=100,
        TacticsMode=200,
        ActionMode=300,
    }
    [SerializeField]
    private SceneState currentState;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //Debug.Log("DontDestroyOnLoad" + gameObject.name);
    }

    private void Start()
    {
        ObjectClick.characterSelectDelegate += ChangeState;
    }

    void ChangeState()
    {
        if (currentState == SceneState.ActionMode)
        currentState = SceneState.TacticsMode;
        else
        {
            currentState = SceneState.ActionMode;
        }
        Debug.Log(currentState);
    }

}
