using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorZone2 : MonoBehaviour
{
    public GameObject door;
    public GameObject fakeDoor;

    public Vector3 doorAngle;

    public MeshRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = true;
        Vector3 temp = new Vector3(10, 60, 10);
        Vector3 angle = this.transform.localEulerAngles;
        fakeDoor = Instantiate(fakeDoor, temp, Quaternion.Euler(angle));
        doorAngle = fakeDoor.gameObject.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
		//this code is for rotatble doors
        if (Input.GetKeyDown("space"))
        {
            Vector3 ang = fakeDoor.gameObject.transform.localEulerAngles;
            ang[1] = ang[1] + 90;
            fakeDoor.gameObject.transform.localEulerAngles = ang;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (GlobalVariables.doorNum <= 0)
        {
            rend.enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
        else
        {
            rend.enabled = true;
            this.gameObject.GetComponent<Collider>().enabled = true;
        }
		

			
		//}
		
		
		//if(hit != this.gameObject.transform){
			
		//}
		
		if(GlobalVariables.doorNum <= 0){
			fakeDoor.gameObject.transform.position = new Vector3(0,-500,0);
		}
    }
	void OnMouseExit(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
		
		if (Physics.Raycast(ray, out hit))
        {
            print("hi");
        }
		
		if(!hit.collider.gameObject.CompareTag("fakeDoor"))
				fakeDoor.gameObject.transform.position = new Vector3(0,-500,0);
	}
			
    void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            print("hi");
        }
            fakeDoor.gameObject.transform.position = Vector3.Lerp(
                fakeDoor.gameObject.transform.position, hit.point, 
                20 * Time.deltaTime);
        Vector3 ang = this.transform.localEulerAngles;
		ang[0] += 90;
        fakeDoor.gameObject.transform.rotation = Quaternion.Euler(ang);
    }

    public void OnMouseDown()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit)) {
                print("hi");
            }
            
            Vector3 mousePos = hit.point;
            
            Vector3 terrainloc = this.transform.position;
			doorAngle = this.transform.localEulerAngles;
            doorAngle[0] += 90;

			Instantiate(door, mousePos, Quaternion.Euler(doorAngle));
			GlobalVariables.doorNum--;
    }
}
