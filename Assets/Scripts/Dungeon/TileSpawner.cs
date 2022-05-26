using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    LayerMask envMask;
    DungeonManager dungeonManager;
    //[SerializeField]
    //GameObject floorHolder;
    //[SerializeField]
    //GameObject wallHolder;
    Vector2 hitsize = Vector2.one * .8f;
    //[SerializeField]
    //GameObject wallHolder;

    private void Awake()
    {
        dungeonManager = FindObjectOfType<DungeonManager>();

        //wallHolder = GameObject.Find("wallGRP");
        //floorHolder = GameObject.Find("FloorGRP");

        if (dungeonManager == null)
        {
            Debug.LogError("<size=14><b>dungeon manager was not found</size></b>");
        }
        else
        {
            GameObject goFloor = Instantiate(dungeonManager.floorprefabs, transform.position, Quaternion.identity);
            goFloor.name = dungeonManager.floorprefabs.name;
            goFloor.transform.SetParent(dungeonManager.tilesHolder.transform);

            if (transform.position.x > dungeonManager.maxX)
            {
                dungeonManager.maxX = transform.position.x;
            }
            if (transform.position.y > dungeonManager.maxY)
            {
                dungeonManager.maxY = transform.position.y;
            }
            if (transform.position.x < dungeonManager.minX)
            {
                dungeonManager.minX = transform.position.x;
            }
            if (transform.position.y < dungeonManager.minY)
            {
                dungeonManager.minY = transform.position.y;
            }
        }
    }
    private void Start()
    {
        envMask = LayerMask.GetMask("Floor", "Wall");
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 targetPos = new Vector2(transform.position.x + x, transform.position.y + y);
                Collider2D hit = Physics2D.OverlapBox(targetPos, hitsize, 0, envMask);
                if (!hit)
                {
                    GameObject goWall = Instantiate(dungeonManager.wallprefabs, targetPos, Quaternion.identity);
                    goWall.name = dungeonManager.wallprefabs.name;
                    goWall.transform.SetParent(dungeonManager.wallsHolder.transform);
                }
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position, Vector3.one);

    }
}
