using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour {
    public Animator animator;
    private int levelToLoad;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"){
            print("hh");
            animator.SetTrigger("Fade");
        }
    }

}
