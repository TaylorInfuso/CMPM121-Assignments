using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallCode : MonoBehaviour
{
	
	public GameObject player;
	public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
					RaycastHit hit;
					if (Physics.Linecast (camera.transform.position, player.transform.position, out hit)) {
						//if(hit.transform.tag != "Sidescroller"){
							Color Ncolor = hit.collider.gameObject.GetComponent<Renderer>().material.color;
							
							Ncolor.a = 0f;print("heyo" + Ncolor.a);
							hit.collider.gameObject.GetComponent<Renderer>().material.color = Ncolor;
							
						//}
		}
					if (!Physics.Linecast (camera.transform.position, transform.position)) {
						Color Ncolor = this.gameObject.GetComponent<Collider>().gameObject.GetComponent<Renderer>().material.color;
						Ncolor.a = 1f;
						this.gameObject.GetComponent<Collider>().gameObject.GetComponent<Renderer>().material.color = Ncolor;
					}
	}
}
