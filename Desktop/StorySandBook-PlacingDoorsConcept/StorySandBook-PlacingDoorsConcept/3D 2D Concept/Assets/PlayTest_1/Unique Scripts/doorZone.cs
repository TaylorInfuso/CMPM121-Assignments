using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorZone : MonoBehaviour
{
    public GameObject door;
    public MeshRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.doorNum <= 0)
        {
            //Destroy(this.GameObject);
            rend.enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
        //else
           // rend.enabled = true;
    }
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 19.0f, Input.mousePosition.z));
            //Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            //print(ray);
            if (Physics.Raycast(ray, out hit)) {
                print("hi");
            }
            //print(hit.point);
            Vector3 mousePos = hit.point;
            Vector3 terrainloc = this.transform.position;

            mousePos[1] = terrainloc[1] - 0.78f;
            Instantiate(door, mousePos, Quaternion.identity);
            //Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            GlobalVariables.doorNum--;
            print(GlobalVariables.doorNum);
            //RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        }
    }
}
