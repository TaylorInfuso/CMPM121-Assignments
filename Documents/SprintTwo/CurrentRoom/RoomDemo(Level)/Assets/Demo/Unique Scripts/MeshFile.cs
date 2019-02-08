using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFile : Component {

	public List <Vector3> newVertices = new List<Vector3>();
	public List <Vector3> newNormals = new List<Vector3>();
	public List <Vector2> newUV = new List<Vector2>();
	public List <int> newTriangles = new List<int>();	
	
	public Mesh mesh = new Mesh();
	//GetComponent<MeshFilter>().mesh = mesh;		

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
