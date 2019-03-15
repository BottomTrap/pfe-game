using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   public enum Phases
    {
        PlayerPhase,
        EnemyPhase,
        PausePhase,
        Window, //some random window that can stop some parts of the game so the player can read it( tutorial UI or character dialogue)
    }

    [SerializeField]
    private int commmandPoints;
    


}
