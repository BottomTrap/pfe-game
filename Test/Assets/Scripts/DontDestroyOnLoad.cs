using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Debug.Log("DontDestroyOnLoad" + gameObject.name);
    }

    private void Start()
    {
       // ObjectClick.characterSelectDelegate += ChangeState;
        //currentState = SceneState.Menu;
    }

    void ChangeState()
    {
        if (currentState == SceneState.ActionMode || currentState== SceneState.Menu)
        currentState = SceneState.TacticsMode;
        else
        {
            currentState = SceneState.ActionMode;
        }
        Debug.Log(currentState);
    }


    private void Update()
    {
        switch (currentState) {
            case SceneState.TacticsMode:
                {
                    SceneManager.LoadScene("Level1");
                    break;
                }
        }
    }

}
