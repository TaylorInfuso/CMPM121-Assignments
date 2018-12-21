using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            Debug.Log("A key or mouse click has been detected");
            Initiate.Fade("Level_1", Color.black, 2.0f);
        }
    }
}
