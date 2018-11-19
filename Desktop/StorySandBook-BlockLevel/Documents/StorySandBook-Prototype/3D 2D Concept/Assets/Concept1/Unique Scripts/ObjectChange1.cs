using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChange1 : MonoBehaviour {

    // Use this for initialization
    Vector3 originalPos;
    Vector3 originalDim;
    Vector3 originalScale;
    private bool flat;
    public float Layer2D = 0;

    void Start () {
        originalPos = this.gameObject.transform.position;
        originalDim = this.gameObject.GetComponent<Renderer>().bounds.size;
        originalScale = this.gameObject.transform.localScale;
        flat = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("f") && flat == false)
        {
            Debug.Log("yeet");
            this.gameObject.transform.Translate(0, 0, -originalPos.z - (Layer2D * 0.5f));
            Vector3 scale = this.gameObject.transform.localScale;
            scale.z = 0.1f;
            this.gameObject.transform.localScale = scale;
            flat = true;
        } else if (Input.GetKeyDown("f") && flat == true)
        {
            Debug.Log("yote");
            this.gameObject.transform.localScale = originalScale;
            this.gameObject.transform.Translate(0, 0, originalPos.z + (Layer2D * 0.5f));
            flat = false;
        }
	}
}
