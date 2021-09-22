using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This example project referenced video tutorial list made by Brackeys on Youtube
//https://www.youtube.com/watch?v=Au8oX5pu5u4&list=PLPV2KyIb3jR5QFsefuO2RlAgWEz6EvVi6&index=4

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    // Start is called before the first frame update
    void Awake()
    {
        this.transform.position = new Vector3(0f,0f,0f);//make sure the player starts at 0 
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey("a"))
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);
        if (Input.GetKey("d"))
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);

        
    }
}
