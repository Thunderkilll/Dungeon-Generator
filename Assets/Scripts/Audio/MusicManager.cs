using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool isExitTrigger = false;
    public int index;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isExitTrigger)
        {
            if (col.gameObject.name == "Player")
            {
                
            }
        }
    }
    
} 
