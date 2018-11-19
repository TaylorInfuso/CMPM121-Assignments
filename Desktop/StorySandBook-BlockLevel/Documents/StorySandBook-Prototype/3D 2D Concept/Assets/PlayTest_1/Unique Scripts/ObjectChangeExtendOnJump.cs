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

public class ObjectChangeExtendOnJump : MonoBehaviour
{
    public GameObject player;

    Quaternion originalRot;
    GameObject thisObj;

    public bool flat = true;
    public float aniSpeed = 20;
    public float scaleY = 10;
    private bool trueFlat;
    public string type;
	public Vector3 size;
	public Vector3 pos;
	public float airY;
	public float groundY;

    // Use this for initialization
    void Start()
    {
		flat = true;
        thisObj = this.gameObject;
        originalRot = this.transform.rotation;
        trueFlat = flat;
		trueFlat = true;
        if (flat == true)
        {
			print("setscale");
            Vector3 scale = this.gameObject.transform.localScale;
            scale.y = 1f / 181f;
			//scale.y = 3f;
            thisObj.transform.localScale = scale;
        }

        size = GetComponentsInChildren<BoxCollider>()[1].size;
        size = new Vector3(size.x, 1, size.z);
        GetComponentsInChildren<BoxCollider>()[1].size = size;
        

        col = flat;
		pos = thisObj.transform.position;
		groundY = pos.y;
		airY = pos.y + 7f;

    }

    // Update is called once per frame
    void Update()
    {
        if (flat == true)
        {
			pos.y = groundY;
			thisObj.transform.position = pos;
            if (thisObj.transform.localScale.y > 0.01f)
            {
                Vector3 scale = this.gameObject.transform.localScale;
                scale.y -= scaleY / aniSpeed;
                if (scale.y <= 0.01f)
                {
                    trueFlat = true;
                    scale.y = 1f / 181f;
					//scale.y = 3f;
                }
                this.gameObject.transform.localScale = scale;
            }
        }
        else if (flat == false)
        {
			pos.y = airY;
			thisObj.transform.position = pos;
            if (trueFlat == true)
                trueFlat = false;
            if (thisObj.transform.localScale.y < scaleY)
            {
                Vector3 scale = this.gameObject.transform.localScale;
                scale.y += scaleY / aniSpeed;
				scale.y = 3.5f;
                this.gameObject.transform.localScale = scale;
            }

        }

        //I'm sure this code can be optimized
        if (player.GetComponent<PlayerMovement2>().flat == true)
        {
            if (GetComponentsInChildren<BoxCollider>() != null)
            {
				print("hi");
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
                    col.enabled = true;
            }
            if (GetComponentsInChildren<CapsuleCollider>() != null)
            {
                foreach (CapsuleCollider col in this.GetComponentsInChildren<CapsuleCollider>())
                    col.enabled = true;
            }
        }
        else if (trueFlat == false)
        {
			print("trueFlat");
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
                    col.enabled = true;
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
				//print("else");
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
			if (thisObj == player.transform.Find("Body").gameObject){
			print("on plat");
            onPlat = 0;
			}
        }

        flat = DimTrig(type);
		if(Input.GetKeyDown("g"))
			onPlat =2;
		
		//if(onPlat == true && thisObj != player.transform.Find("Body").gameObject){
		//if( thisObj.isTouching(player))
		if(onPlat == 1){
			print("not onPlat");
			col = !col;
			onPlat = 2;
		}
		
		if (thisObj == player.transform.Find("Body").gameObject && player.GetComponent<PlayerMovement2>().flat == false){
			print("on plat");
            onPlat = 0;
		}
			}


    //SPECIFY TYPE SPECIFIC BEHAVIORS HERE
    private bool col;
	private int onPlat = 2;
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
        //if (other.gameObject == player.transform.Find("Body").gameObject && player.GetComponent<PlayerMovement2>().flat == true){
			if (other.gameObject == player.transform.Find("Body").gameObject){
			print("on plat");
            onPlat = 0;
			//col = !col;
		}
		//onPlat = 1;
    }
	
	private void OnTriggerExit(Collider other)
	{
		onPlat = 1;
	}
	
	private void onCollisionEnter(Collision test){
		//if (test.gameObject == player.transform.Find("Body").gameObject && player.GetComponent<PlayerMovement2>().flat == true){
			print("on plat");
            onPlat = 0;
		//}
	}
}