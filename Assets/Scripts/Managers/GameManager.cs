using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
      
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       
    }
    #endregion

    #region ressources

    public List<Sprite> playerSprites;
    public List<Sprite> gameSprites;
    public List<float> xpTable;

    #endregion

    #region References

    public Player player;
    public int moneyAmount;
    public float experience;

    #endregion

    public void SaveState()
    {

    }

    public void LoadState()
    {

    }
  
}
