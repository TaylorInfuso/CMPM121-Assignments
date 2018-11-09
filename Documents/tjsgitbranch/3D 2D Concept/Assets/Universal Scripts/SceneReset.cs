using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReset : MonoBehaviour {
    // Update is called once per frame
    public string levelName;
	void Update () {
        if (Input.GetKeyDown("r"))
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);

	}
}
