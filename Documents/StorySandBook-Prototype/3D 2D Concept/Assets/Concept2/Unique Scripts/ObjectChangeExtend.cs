using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ATTACHED TO EXTENDING BLOCK PREFAB
 * In editor, you may specify whether object will begin flat, the speed of its animation, and the scale of size the object may become(scale Y)
 * You may also specify the string 'type'. This will function as a tag.
 *      Type will help for creating unique triggers for the dimension change.
 *      Visit DimTrigger at the bottom of this file to see an example of and implement your own trigger.
 *      
 * MAKE SURE TO ATTACH THE INSTANTIATED PLAYER OBJECT TO EVERY NEW INSTANCE OF THIS PREFAB
 */

public class ObjectChangeExtend : MonoBehaviour
{

    public GameObject player;

    Quaternion originalRot;
    GameObject thisObj;

    public bool flat = false;
    public float aniSpeed = 20;
    public float scaleY = 10;
    private bool trueFlat;
    public string type;

    // Use this for initialization
    void Start()
    {
        thisObj = this.gameObject;
        originalRot = this.transform.rotation;
        trueFlat = flat;
        if (flat == true)
        {
            Vector3 scale = this.gameObject.transform.localScale;
            scale.y = 1f / 181f;
            thisObj.transform.localScale = scale;
        }

        Vector3 size = GetComponentsInChildren<BoxCollider>()[1].size;
        size = new Vector3(size.x, 1000, size.z);
        GetComponentsInChildren<BoxCollider>()[1].size = size;
        

        col = flat;

    }

    // Update is called once per frame
    void Update()
    {
        if (flat == true)
        {
            if (thisObj.transform.localScale.y > 0.01f)
            {
                Vector3 scale = this.gameObject.transform.localScale;
                scale.y -= scaleY / aniSpeed;
                if (scale.y <= 0.01f)
                {
                    trueFlat = true;
                    scale.y = 1f / 181f;
                }
                this.gameObject.transform.localScale = scale;
            }
        }
        else if (flat == false)
        {
            if (trueFlat == true)
                trueFlat = false;
            if (thisObj.transform.localScale.y < scaleY)
            {
                Vector3 scale = this.gameObject.transform.localScale;
                scale.y += scaleY / aniSpeed;
                this.gameObject.transform.localScale = scale;
            }

        }

        //I'm sure this code can be optimized
        if (player.GetComponent<PlayerMovement2>().flat == true)
        {
            if (GetComponentsInChildren<BoxCollider>() != null)
            {
                foreach (BoxCollider col in this.GetComponentsInChildren<BoxCollider>())
                    col.enabled = true;
            }
            if (GetComponentsInChildren<SphereCollider>() != null)
            {
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
            if (GetComponentsInChildren<BoxCollider>() != null)
            {
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


    //SPECIFY TYPE SPECIFIC BEHAVIORS HERE
    private bool col;
    bool DimTrig(string type)
    {
        if (type == "example")
        {
            return col;
        }

        if (type == "alternate")
        {
            if (this.gameObject.transform.localScale.y == 1f / 181f || this.gameObject.transform.localScale.y >= scaleY)
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