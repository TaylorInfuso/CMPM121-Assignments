/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimTrigger : MonoBehaviour {

    public GameObject player;

    private void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ObjectType1"))
        {
            //Debug.Log("Come one, come all");
            obj.GetComponent<ObjectChangeRotate>().DimTrigger = this.gameObject;
        }
        //foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ObjectType2"))
        //    obj.GetComponent<ObjectChangeExtend>().DimTrigger = this.gameObject;
    }

    public bool triggered(string tag, bool flat, GameObject obj)
    {

        if (tag == "tree")
        {

        }

        if (tag == "example")
        {
            foreach (Collider collider in obj.GetComponentsInChildren<Collider>()) {
                //Debug.Log("test");
                ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
                cb.Initialize(this);
                //Debug.Log(cb.col);
                if (cb.col != null)
                {
                    if (cb.col.gameObject == player)
                    {
                        Debug.Log("I wonder");
                        return !flat;
                    }
                }

            }
            return flat;
        }



        if (Input.GetKeyDown("k"))
            flat = !flat;
        return flat;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Necessary for collisions
    }

    public void OnCollisionExit(Collision collision)
    {
        //Necessary for collisions
    }
}
*/