using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMove : MonoBehaviour
{
    //Saves a reference to the CollectableManager script
    [SerializeField]
    GameObject CollectableManager;

    int numOfCollected = 0;

    private bool movingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        CollectableManager = GameObject.Find("CollectableManager");
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the cube in place constantly
        transform.Rotate(Vector3.right, 200 * Time.deltaTime);

        //Move left when Collectable hits left wall
        if (movingLeft == true)
        {
            transform.Translate(-0.004f, 0f, 0f);
            if (transform.position.x <= -1f)
            {
                transform.Translate(0.004f, 0f, 0f);
                movingLeft = false;
            }
        }

        //Move right when Collectable hits right wall
        else 
        {
            transform.Translate(0.004f, 0f, 0f);
            if (transform.position.x >= 1f)
            {
                transform.Translate(-0.004f, 0f, 0f);
                movingLeft = true;
            }
        }
    }

    //Unity will call this function when a collider hits the collider of the CollectableMove object
    void OnTriggerExit(Collider other)
    {
        //If player colides with a Collectable
        if (CollectableManager != null && other.name == "Player")
        {
            float playerZpos = transform.position.z;
            //find the TrackManager.cs script and call its public function
            //TrackManager.gameObject.GetComponent<TrackManager>().TileAdjustment(this.gameObject);
            CollectableManager.gameObject.GetComponent<CollectableManager>().DeleteCollectable(playerZpos);
            CollectableManager.gameObject.GetComponent<CollectableManager>().Collected();
        }
    }
}
