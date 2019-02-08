using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshHitbox : MonoBehaviour
{
    public int num;
    public GameObject terrain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collider)
    {
        //if (collider.gameObject.tag == ("Tree"))
        //{
            print("yaaaaaaa");
            terrain.gameObject.GetComponent<TerrainGenerator>().lowerVertices.Add(num);
        //}
    }

    void OnCollisionExit(Collision collider)
    {
        //if (collider.gameObject.CompareTag("cylinderHitbox"))
        //{
        //print("naaaaaaa");
            terrain.gameObject.GetComponent<TerrainGenerator>().higherVertices.Add(num);
        //}
    }
}
