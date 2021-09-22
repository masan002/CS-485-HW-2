using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Reference to Rigidbody Component
    public Rigidbody rb;
    
    //Reference to lock Z rotation of our player
    private Quaternion lockedRotation;

    void Start()
    {
        //Initallize our variable to transform.rotation
        lockedRotation = transform.rotation;
    }

    //Fixed update works better than update in Unity
    //Especially when dealing with physics
    void FixedUpdate()
    {
        if (transform.position.y > 0.4f)
        {
            transform.position = new Vector3(transform.position.x, 0.4f, transform.position.z);
        }

        //Lock the rotation of our cube
        transform.rotation = lockedRotation;

        //Adding a constant force moving foward but maintain z velocity
        if(rb.velocity.z < 8)
            rb.AddForce(0, 0, 350 * Time.deltaTime);

        if (Input.GetKey("a"))
            rb.AddForce(-350 * Time.deltaTime, 0, 0);
        
        if (Input.GetKey("d"))
            rb.AddForce(350 * Time.deltaTime, 0, 0);
        
    }
}
