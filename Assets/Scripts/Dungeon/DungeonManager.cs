using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum DungeonType { Cavern, prison, WindingHaull }

public class DungeonManager : MonoBehaviour
{
    [Space]
    [Header("   ++++  Dungeon Settings ++++")]
    [Tooltip("Determine if we want to use the rounded edges prefabs on top of the walls or not to stimulate a dungeon corner")]
    [SerializeField]
    bool useRoundEdges = false;
    [SerializeField]
    [Range(3, 27)]
    int maxRoomSize = 9;
    [SerializeField]
    [Range(3, 27)]
    int minRoomSize = 3;
    [SerializeField]
    [Range(0,100)]
    private int windingHaullPecent;
    [Tooltip("Decides which type of Dungeon we want")]
    public DungeonType type;
    [Header("   ++++  Prefabs Settings ++++")]
    [Space]
    public GameObject[] randomItemsPrefabs;
    public GameObject[] randomEnemiesPrefabs;
    [Tooltip("list of rounded edges tiles")]
    public GameObject[] roundEdgesWallPrefabs;
    [Space]
    public GameObject wallprefabs;
    public GameObject floorprefabs;
    public GameObject tilePrefab;
    public GameObject exitDoorPrefab;

    [Header("   ++++  Tiles Holders Settings ++++")]
    [Space]
    public GameObject tilesHolder;
    public GameObject wallsHolder;
    public GameObject itemsHolder;

    [Header("    ++++ Floors Settings ++++")]
    [Space]
    public int totalFloorCount;
    [Range(0, 100)]
    public float itemSpawnPercent;
    [Range(0, 100)]
    public float enemySpawnPercent;

    [HideInInspector]
    public float minX, maxX, minY, maxY;


    //local variables 

    Vector2 hitsize = Vector2.one * .8f;
    LayerMask floorMask, wallMask;
    List<Vector3> floorList = new List<Vector3>();
    

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        wallMask = LayerMask.GetMask("Wall");
        //choose between rooms-hallways or caves Generators


        switch (type)
        {
            case DungeonType.Cavern:
                CaveWalker();
                break;
            case DungeonType.prison:
                PrisonWalker();
                break;
            case DungeonType.WindingHaull:
                WindingWalker();
                break;
            default:
                break;
        }


    }



    private void Update()
    {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    private void WindingWalker()
    {
        Vector3 currPos = Vector3.zero;
        //set floor tile at currposition 
        floorList.Add(currPos);

        while (floorList.Count < totalFloorCount)
        {
             
            currPos = TakeAHike(currPos);
            int roll = UnityEngine.Random.Range(1, 100);

            if (roll > windingHaullPecent)
                RandomRoom(currPos);

        }

        StartCoroutine(DelayProgress());
    }


    /// <summary>
    /// create random generated map of a prison like dungeon that is men made 
    /// </summary>
    private void PrisonWalker()
    {
        Vector3 currPos = Vector3.zero;
        //set floor tile at currposition 
        floorList.Add(currPos);

        while (floorList.Count < totalFloorCount)
        {
            currPos = TakeAHike(currPos);

            RandomRoom(currPos);
        }



        StartCoroutine(DelayProgress());
    }

    Vector3 TakeAHike(Vector3 myPos)
    {
        Vector3 walkDir = RandomPosition();
        int walkLength = UnityEngine.Random.Range(minRoomSize, maxRoomSize); 

        for (int i = 0; i < walkLength; i++)
        {
           
            if (!InFloorList(myPos + walkDir))
            {
                floorList.Add(myPos + walkDir);
            }
            myPos += walkDir;
        }
        return myPos;
      
    }

    /// <summary>
    /// Generate dungeon through an invisible player object that walk around the
    /// map and each tile he walks on it is considered a floor tile.
    /// Create an organic cave like dungeon (done) 
    /// </summary>
    void CaveWalker()
    {
        Vector3 currPos = Vector3.zero;
        //set floor tile at currposition 
        floorList.Add(currPos);

        while (floorList.Count < totalFloorCount)
        {

            currPos += RandomPosition(); 

            bool inFloorList = false;

            inFloorList = InFloorList(currPos);
            if (!inFloorList)
            {
                floorList.Add(currPos);
            }
        }

        StartCoroutine(DelayProgress());
    }



    /// <summary>
    ///   create rooms in prison map and winding map
    /// </summary>

 void RandomRoom(Vector3 currPos)
    {
        //at the end of the walk we will make a room 
        int width = UnityEngine.Random.Range(1, 5);
        int length = UnityEngine.Random.Range(1, 5);
        //Debug.Log(" room length <color=blue><size=13>" + width + "</size></color> X <color=blue><size=13>" + length + "</size></color>");

        for (int w = -width; w <= width; w++)
        {
            for (int h = -length; h <= length; h++)
            {
                Vector3 offset = new Vector3(w, h, 0);
                
                if (!InFloorList(currPos + offset))
                {
                    floorList.Add(currPos + offset);
                }
            }
        }

    
    }

    private bool InFloorList(Vector3 pos)
    {
        for (int i = 0; i < floorList.Count; i++)
        {
            if (Vector3.Equals(pos, floorList[i]))
            {
                return true;

            }

        }

        return false;
    }

    private Vector3 RandomPosition()
    {
        switch (UnityEngine.Random.Range(1, 5))
        {
            case 1:
                return Vector3.up;

            case 2:
                return Vector3.right;

            case 3:
                return Vector3.down;

            case 4:
                return Vector3.left;
        }
        return Vector3.zero;

    }

    IEnumerator DelayProgress()
    {

        for (int i = 0; i < floorList.Count; i++)
        {
            GameObject goTile = Instantiate(tilePrefab, floorList[i], Quaternion.identity);
            goTile.name = wallprefabs.name;
            goTile.transform.SetParent(tilesHolder.transform);
        }

        while (FindObjectsOfType<TileSpawner>().Length > 0)
        {

            yield return null;
        }
        CreateDoorWay();
        for (int x = (int)minX - 2; x <= maxX + 2; x++)
        {
            for (int y = (int)minY - 2; y <= maxY + 2; y++)
            {
                Collider2D hit = Physics2D.OverlapBox(new Vector2(x, y), hitsize, 0, floorMask);
                if (hit)
                {
                    if (!(Vector2.Equals(hit.transform.position, floorList[floorList.Count - 1])))
                    {
                        Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), hitsize, 0, wallMask);
                        Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y), hitsize, 0, wallMask);
                        Collider2D hitButtom = Physics2D.OverlapBox(new Vector2(x, y - 1), hitsize, 0, wallMask);
                        Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), hitsize, 0, wallMask);
                        RandomItems(hit, hitTop, hitRight, hitButtom, hitLeft);
                        RandomEnemies(hit, hitTop, hitRight, hitButtom, hitLeft);
                    }

                }
                RandomEdges(x, y);
            }
        }
    }

    private void RandomEdges(int x, int y)
    {
        if (useRoundEdges)
        {//create rounded edges here 
            Collider2D hitWall = Physics2D.OverlapBox(new Vector2(x, y), hitsize, 0, wallMask);
            if (hitWall)
            {
                Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), hitsize, 0, wallMask);
                Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y), hitsize, 0, wallMask);
                Collider2D hitButtom = Physics2D.OverlapBox(new Vector2(x, y - 1), hitsize, 0, wallMask);
                Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), hitsize, 0, wallMask);
                int bitValue = 0;
                if (!hitTop)
                    bitValue += 1;
                if (!hitRight)
                    bitValue += 2;
                if (!hitButtom)
                    bitValue += 4;
                if (!hitLeft)
                    bitValue += 8;

                if (bitValue > 0)
                {
                    GameObject goEdge = Instantiate(roundEdgesWallPrefabs[bitValue], new Vector2(x, y), Quaternion.identity);
                    goEdge.transform.SetParent(wallsHolder.transform);
                    goEdge.name = roundEdgesWallPrefabs[bitValue].name;
                }

            }

        }
    }

    private void RandomEnemies(Collider2D hit, Collider2D hitTop, Collider2D hitRight, Collider2D hitButtom, Collider2D hitLeft)
    {
        if (!hitTop && !hitButtom && !hitLeft && !hitRight)
        {
            float roll = UnityEngine.Random.Range(1, 101);
            if (roll <= enemySpawnPercent)
            {
                int enemyIndex = UnityEngine.Random.Range(0, randomEnemiesPrefabs.Length);
                GameObject goItem = Instantiate(randomEnemiesPrefabs[enemyIndex], hit.transform.position, Quaternion.identity);
                goItem.name = randomEnemiesPrefabs[enemyIndex].name;
                goItem.transform.SetParent(hit.transform);

            }
        }
    }


    /// <summary>
    /// instantiate a random item with a random chance of being spawned 
    /// </summary>
    /// <param name="hit"></param>
    private void RandomItems(Collider2D hit, Collider2D hitTop, Collider2D hitRight, Collider2D hitButtom, Collider2D hitLeft)
    {
        if ((hitTop || hitButtom || hitRight || hitLeft) && !(hitTop && hitButtom) && !(hitRight && hitLeft))
        {
            float roll = UnityEngine.Random.Range(1, 101);
            if (roll <= itemSpawnPercent)
            {
                int itemIndex = UnityEngine.Random.Range(0, randomItemsPrefabs.Length);
                GameObject goItem = Instantiate(randomItemsPrefabs[itemIndex], hit.transform.position, Quaternion.identity);
                goItem.name = randomItemsPrefabs[itemIndex].name;
                goItem.transform.SetParent(hit.transform);
            }
        }
    }

    private void CreateDoorWay()
    {
        var random = new System.Random();
        int index = random.Next(floorList.Count);
        Vector3 doorPos = floorList[index];
        GameObject goDoor = Instantiate(exitDoorPrefab, doorPos, Quaternion.identity);
        goDoor.name = exitDoorPrefab.name;
        goDoor.transform.SetParent(gameObject.transform);
    }
}




/*
  private void PrisonWalker()
    {
        Vector3 currPos = Vector3.zero;
        //set floor tile at currposition 
        floorList.Add(currPos);

        while (floorList.Count < totalFloorCount)
        {

            
            Vector3 walkDir = RandomPosition();
            int walkLength = UnityEngine.Random.Range(minRoomSize, maxRoomSize);

            bool inFloorList = false;

            for (int i = 0; i < walkLength; i++)
            {
                inFloorList = InFloorList(currPos + walkDir);
                if (!inFloorList)
                {
                    floorList.Add(currPos + walkDir);
                }
                currPos += walkDir;
                //at the end of the walk we will make a room 
                int width  = UnityEngine.Random.Range(1, 5);
                int length = UnityEngine.Random.Range(1, 5);
                //Debug.Log(" room length <color=blue><size=13>" + width + "</size></color> X <color=blue><size=13>" + length + "</size></color>");
               
                for (int w = -width; w <= width; w++)
                {
                    for (int h = -length; h <= length; h++)
                    {
                        offset = new Vector3(w, h, 0);
                        inFloorList = InFloorList(currPos + offset);
                        if (!inFloorList)
                        {
                            floorList.Add(currPos + offset);
                        }
                    }
                }
            }
        
        }

        StartCoroutine(DelayProgress());
    }
*/