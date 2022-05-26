using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class ExitDoor : MonoBehaviour
{
    BoxCollider2D box;
    private void Reset()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        box = GetComponent<BoxCollider2D>();
        box.size = Vector2.one * .1f;
        box.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Press F");
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }


        }
    }
  
}
