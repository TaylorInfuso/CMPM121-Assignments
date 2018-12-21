using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimation : MonoBehaviour {
    public Animator leafOut;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("hh");
            gameObject.SetActive(false);
            leafOut.SetTrigger("Leaf_Out");
        }
    }
}
