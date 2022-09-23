using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Explore,
    Warning,
    Combat
}
public class GameStateManager : MonoBehaviour
{
    #region Singleton 
    public static GameStateManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    #endregion

    public static GameState currentState;

    void Start()
    {
        currentState = GameState.Explore;
    }

    /// <summary>
    /// This function sets the current game state
    /// then will be used to change the audio corresponding to it 
    /// </summary>
    public void SetGameState(float setState)
    {
        if (setState == 0.0f)
        {
            currentState = GameState.Explore;
            Debug.Log("<color=green>Exploring</color>");
        }
        else if (setState == 1.0f)
        {
            currentState = GameState.Warning;
            Debug.Log("<color=yellow>Be Careful something off</color>");

        }
        else if (setState == 2.0f)
        {
            currentState =GameState.Combat;
            Debug.Log("<color=red>Fight for your life</color>");
        }
        else
        {
            
        }
    }

}
