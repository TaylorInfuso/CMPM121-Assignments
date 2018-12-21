using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ATTACHED TO ROTATE PARENT
 * In editor, you may specify whether object will begin flat and the speed of its animation
 * You may also specify the string 'type'. This will function as a tag.
 *      Type will help for creating unique triggers for the dimension change.
 *      Visit DimTrigger at the bottom of this file to see an example of and implement your own trigger.
 *      
 * Make sure to attach a mesh collider and then box collider, capsule collider and/or sphere to every child object you wish to collide with.
 *      
 * MAKE SURE TO ATTACH THE INSTANTIATED PLAYER OBJECT TO EVERY NEW INSTANCE OF THIS PREFAB
 */

public class ObjectChangeRotate : MonoBehaviour
{

    public GameObject player;

    Quaternion originalRot;
    GameObject thisObj;

    public bool flat = false;
    public float aniSpeed = 20;
    private bool trueFlat;
    public string type;

    // Use this for initialization
    void Start()
    {
        thisObj = this.gameObject;
        originalRot = this.transform.rotation;
        trueFlat = flat;
        if(flat == true)
        {
            thisObj.transform.Rotate(90 - originalRot.x, 0, 0);
            Vector3 scale = this.gameObject.transform.localScale;
            scale.z = 1f / 181f;
            thisObj.transform.localScale = scale;
        }

        foreach(BoxCollider boxC in GetComponentsInChildren<BoxCollider>())
        {
            Vector3 size = boxC.size;
            size = new Vector3(size.x, size.y, 1000);
            boxC.size = size;
        }

        col = flat;

    }

    // Update is called once per frame
    void Update()
    {
        if (flat == true)
        {

            if (thisObj.transform.eulerAngles.x < 89.9)
            {
                this.gameObject.transform.Rotate((90 - originalRot.x)/aniSpeed, 0, 0);
                if (thisObj.transform.eulerAngles.x > 88)
                    thisObj.transform.eulerAngles = new Vector3(90f, 0, 0);
            }
            if (thisObj.transform.localScale.z > 0.01f)
            {
                Vector3 scale = this.gameObject.transform.localScale;
                scale.z -= 1f / aniSpeed;
                if (scale.z <= 0.01f)
                {
                    trueFlat = true;
                    scale.z = 1f / 181f;
                }
                this.gameObject.transform.localScale = scale;
            }
        }
        else if (flat == false && thisObj.transform.eulerAngles.x > 0.1)
        {
            if (trueFlat == true)
                trueFlat = false;
            this.gameObject.transform.Rotate(-(90 - originalRot.x)/ aniSpeed, 0, 0);
            Vector3 scale = this.gameObject.transform.localScale;
            scale.z += 1f / aniSpeed;
            this.gameObject.transform.localScale = scale;
            
        }

        //I'm sure this code can be optimized
        if (player.GetComponent<PlayerMovement2>().flat == true && trueFlat == true) {
            if (GetComponentsInChildren<BoxCollider>() != null)
            {
                foreach (BoxCollider col in this.GetComponentsInChildren<BoxCollider>())
                    col.enabled = true;
            }
            if (GetComponentsInChildren<SphereCollider>() != null) {
                foreach (SphereCollider col in this.GetComponentsInChildren<SphereCollider>())
                    col.enabled = true;
            }
            if (GetComponentsInChildren<MeshCollider>() != null)
            {
                foreach (MeshCollider col in this.GetComponentsInChildren<MeshCollider>())
                    col.enabled = false;
            }
            if (GetComponentsInChildren<CapsuleCollider>() != null)
            {
                foreach (CapsuleCollider col in this.GetComponentsInChildren<CapsuleCollider>())
                    col.enabled = false;
            }
        }
        else if (trueFlat == false)
        {
            if (GetComponentsInChildren<SphereCollider>() != null)
            {
                foreach (SphereCollider col in this.GetComponentsInChildren<SphereCollider>())
                    col.enabled = false;
            }
            if (GetComponentsInChildren<CapsuleCollider>() != null)
            {
                foreach (CapsuleCollider col in this.GetComponentsInChildren<CapsuleCollider>())
                    col.enabled = false;
            }
            if (GetComponentsInChildren<BoxCollider>() != null)
            {
                foreach (BoxCollider col in this.GetComponentsInChildren<BoxCollider>())
                    col.enabled = false;
            }
            if (GetComponentsInChildren<MeshCollider>() != null)
            {
                foreach (MeshCollider col in this.GetComponentsInChildren<MeshCollider>())
                    col.enabled = true;
            }
        }
        else 
        {
            if (GetComponentsInChildren<BoxCollider>() != null) {
                foreach (BoxCollider col in this.GetComponentsInChildren<BoxCollider>())
                    col.enabled = false;
            }
            if (GetComponentsInChildren<SphereCollider>() != null)
            {
                foreach (SphereCollider col in this.GetComponentsInChildren<SphereCollider>())
                    col.enabled = false;
            }
            if (GetComponentsInChildren<CapsuleCollider>() != null)
            {
                foreach (CapsuleCollider col in this.GetComponentsInChildren<CapsuleCollider>())
                    col.enabled = false;
            }
            if (GetComponentsInChildren<MeshCollider>() != null)
            {
                foreach (MeshCollider col in this.GetComponentsInChildren<MeshCollider>())
                    col.enabled = false;
            }
        }

        flat = DimTrig(type);
    }


    private bool col;
    bool DimTrig(string type)
    {
        if (type == "example")
        {
            return col;
        }

        if (type == "alternate")
        {
            if (this.gameObject.transform.localScale.z == 1f / 181f || this.gameObject.transform.localScale.z >= 1)
                flat = !flat;
        }

        if (Input.GetKeyDown("k"))
            flat = !flat;

        return flat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.transform.Find("Body").gameObject && player.GetComponent<PlayerMovement2>().flat == true)
            col = !col;
    }
}
