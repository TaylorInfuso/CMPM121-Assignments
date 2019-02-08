using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {

    public bool flat;
    private Vector3 newAngle;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {

        if (flat == false)
        {
           transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * 5); 
        }
        

        //still working on the 2d pickups
        if(flat == true)
        {
            //rb.angularVelocity.y = 0.0f;
            //rb.angularVelocity.z = 0.0f;
            //rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


            Vector3 scale = this.transform.localScale;
            scale.x = 1 / 181f;
            this.transform.localScale = scale;

            newAngle = new Vector3 (0,0,90);
            this.transform.eulerAngles = newAngle;
           
            //transform.Rotate(15 * Time.deltaTime * 5, 0, 0);
        }
        //transform.Rotate(new Vector3(15, 0, 0) * Time.deltaTime * 5);
        
        
    }
}
