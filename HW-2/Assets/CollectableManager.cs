using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    //List saves all Collectable prefabs
    public List<GameObject> CollectablePrefabs;

    //saves the player object
    public GameObject player;

    //List saves a pool of Collectables to reuse
    [SerializeField]
    private List<GameObject> CollectablesPool = new List<GameObject>();

    //List saves the existing tiles
    [SerializeField]
    private List<GameObject> Collectables = new List<GameObject>();

    //saves the total number of Collectables generated
    [SerializeField]
    private int numCollectables = 0;

    //Saves the total number of Collectables currently on the track
    [SerializeField]
    private int numCurrentCollectables = 0;

    //saves the tile has the player on it
    [SerializeField]
    private int curTileWithPlayer;

    // Start is called before the first frame update
    void Start()
    {
        AddACollectable(10);
        AddACollectable(20);

        numCollectables = numCollectables - 2;
    }

    // Update is called once per frame
    void Update()
    {
       //Find the collectables and players Z position
        float CollectableZpos = Collectables[0].transform.position.z;
        float playerZpos = player.transform.position.z;
            
        //The player missed this collectable, delete the first collectable in the list
        if ( playerZpos - CollectableZpos > 1 )
        {
            DeleteCollectable(playerZpos);
            Debug.Log("Collectable Number " + numCollectables + " missed");
        }

        //Add a collectable to the track
        //So long as the number of current collectables on the track is less than 2
        else 
        {
            if(numCurrentCollectables < 2)
                {

                //Generate a random Z pos for the Collectable to spawn on
                float randomZPos = Random.Range(10, 15);
                playerZpos = playerZpos + randomZPos;

                AddACollectable(playerZpos);
            }
        }
    }


    //Utility Functions
    //------------------------------------------------------------------------
    void AddACollectable(float zPos)
    {
        //Obtain a random collectable prefab index
        int randomCollectableIndex = Random.Range(0, CollectablePrefabs.Count);

        //Create a new Collectable by making a copy of the saved prefab object
        GameObject newCollectable = Instantiate(CollectablePrefabs[randomCollectableIndex]);

        //Generate a random X pos for the Collectable to spawn on
        float randomXPos = Random.Range(-1, 1);

        //Position the Collectable
        newCollectable.transform.position = new Vector3(randomXPos, 0.6f, zPos);

        //Update the Collectable object to the child of the CollectableManager
        newCollectable.transform.parent = this.transform;
        numCollectables++;

        newCollectable.transform.name = numCollectables.ToString() + newCollectable.transform.name;
        Collectables.Add(newCollectable);

        numCurrentCollectables++;
    }

    public void DeleteCollectable(float zPos)
    {
        GameObject CollectableToRemove = Collectables[0];
        Collectables.RemoveAt(0);
        GameObject.Destroy(CollectableToRemove);
        numCurrentCollectables = numCurrentCollectables - 1;

        AddACollectable(zPos + 20);
    }

    //Find out which tile the player is on
    //Each tile is 2m long in Z. The 1st tile will be at z=0-2,  2nd tile be at z=2-4...
    int PlayerOnTileIndex()
    {
        int index = 0;
        float playerZpos = player.transform.position.z;

        index = Mathf.FloorToInt(playerZpos / 2);
        return index;
    }

    public void Collected()
    {
        Debug.Log("Collectable Number " + numCollectables + " hit");
    }
}
