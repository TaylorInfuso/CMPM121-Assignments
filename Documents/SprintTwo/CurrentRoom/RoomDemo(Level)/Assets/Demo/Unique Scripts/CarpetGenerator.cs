using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetGenerator : MonoBehaviour
{
	
	public GameObject carpet;
    // Start is called before the first frame update
    void Start()
    {
		for(int i = -2; i < 26; i++){
			for(int j = 0; j < 90; j++){
					Vector3 pos = new Vector3(31.64f + j, -2.02f, 19 + i);
					Vector3 rot = new Vector3 (-90,0,0);
					Instantiate(carpet.gameObject, pos, Quaternion.Euler(rot));
					if(j == 79)
						i++;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
