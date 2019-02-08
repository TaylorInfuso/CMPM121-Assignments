using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject hitbox;

    private int length = 50;
    private int height = 50;

    [Range(1.0f, 6.0f)]
    public int octaves = 3;

    [Range(0.05f, 50.0f)]
    public float bumpiness = 0.05f;

    private float[] heightMap = new float[10000];

    private int cur = 0;
    public List<Vector3> newVertices = new List<Vector3>();
    public List<Vector3> newNormals = new List<Vector3>();
    public List<Vector2> newUV = new List<Vector2>();
    public List<int> newTriangles = new List<int>();
    public List<Vector3> changeableVertices = new List<Vector3>();
    public List<int> changeableTriangles = new List<int>();

    public List<int> lowerVertices = new List<int>();
    public List<int> higherVertices = new List<int>();

    // Use this for initialization
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 50; j++)
            {

                newVertices.Add(new Vector3(j, 1, i));
                newNormals.Add(new Vector3(0, 0, 1));
                newVertices.Add(new Vector3(j + 1, 1, i));
                newNormals.Add(new Vector3(0, 0, 1));
                newVertices.Add(new Vector3(j, 1, i + 1));
                newNormals.Add(new Vector3(0, 0, 1));
                newVertices.Add(new Vector3(j + 1, 1, i + 1));
                newNormals.Add(new Vector3(0, 0, 1));

                int[] vOrder = new int[] { cur, cur + 2, cur + 1, cur + 2, cur + 3, cur + 1 };
                for (int k = 0; k < 6; k++)
                {
                    newTriangles.Add(vOrder[k]);
                }

                newUV.Add(new Vector2(0.0f, 0.0f));
                newUV.Add(new Vector2(0.0f, 1.0f));
                newUV.Add(new Vector2(1.0f, 0.0f));
                newUV.Add(new Vector2(1.0f, 0.0f));
                newUV.Add(new Vector2(0.0f, 1.0f));
                newUV.Add(new Vector2(1.0f, 1.0f));

                cur = cur + 4;
            }
        }



        //this portion was originally in update
        for (int k = 0; k < 5000; k++)
        {
            float yPerl = calculateHeight(newVertices[k].x, newVertices[k].z);
            newVertices[k] = new Vector3(newVertices[k].x, yPerl, newVertices[k].z);
            heightMap[k] = yPerl;
            //Vector3 temp = new Vector3(0, 0, 0);

            Vector3 temp = newVertices[k];
            temp[1] = 10;

            GameObject cylinder;
            cylinder = Instantiate(hitbox, temp, Quaternion.identity);
            cylinder.gameObject.GetComponent<meshHitbox>().num = k;
        }
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.normals = newNormals.ToArray();
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print("Our Vertice count: " + lowerVertices.Count);
        if ((lowerVertices.Count > 0 || higherVertices.Count > 0) && (player.GetComponent<PlayerMovement2>().rb.velocity.z != 0
            || player.GetComponent<PlayerMovement2>().bottom2D == false))
        {
            //lower();
            //print("our bottom2D is: " + player.GetComponent<PlayerMovement2>().bottom2D + "our x velocity is : " + player.GetComponent<PlayerMovement2>().rb.velocity.z);
        }

        //if (Input.GetKeyDown("space"))
        //{


        //Mesh mesh = new Mesh();
        //GetComponent<MeshFilter>().mesh = mesh;
        //cur = 0;
        //if (player.GetComponentInParent<PlayerMovement2>().flat == true)
        //{
        //    for (int k = 0; k < 5000; k++)
        //    {
        //        if (Mathf.Abs(newVertices[k].x - player.transform.position.x) + Mathf.Abs(newVertices[k].z - player.transform.position.z) < 5)
        //        {
        //            print("whaddup");
        //            newVertices[k] = new Vector3(newVertices[k].x, -400, newVertices[k].z);
        //        }
        //        else
        //        {
        //            newVertices[k] = new Vector3(newVertices[k].x, heightMap[k], newVertices[k].z);
        //        }
        //    }
        //}

        //mesh.vertices = newVertices.ToArray();
        //mesh.triangles = newTriangles.ToArray();
        //mesh.normals = newNormals.ToArray();
        //mesh.RecalculateNormals();


        //}
    }

    float calculateHeight(float x, float y)
    {
        float noiseXstart = x;
        float noiseYstart = y;
        float edgeSize = 1;
        float noiseVal = 0.0f;
        float frequency = 1.0f;
        float amplitude = 1.0f;
        float maxValue = 0.0f;
        float persistence = 1.0f;
        for (int i = 0; i < octaves; i++)
        {
            //noiseXspan and noiseYspan refer to the range of change
            float perlinX = noiseXstart + ((float)x / (float)edgeSize) * (1.0f / bumpiness) * frequency;
            float perlinY = noiseYstart + ((float)y / (float)edgeSize) * (1.0f / bumpiness) * frequency;

            noiseVal += Perlin.Noise(perlinX, perlinY, 0.0f) * amplitude;

            maxValue += amplitude;
            amplitude *= persistence;
            frequency *= 2;
        }
        return noiseVal / maxValue;
    }
    private void lower()
{
        print("fuck man");
    Mesh mesh = new Mesh();
    GetComponent<MeshFilter>().mesh = mesh;

    int[] vals = new int[lowerVertices.Count];
    vals = lowerVertices.ToArray();

    for (int i = 0; i < lowerVertices.Count; i++)
        {
            newVertices[vals[i]] = new Vector3(newVertices[vals[i]].x, -400, newVertices[vals[i]].z);
            //print(newVertices[vals[i]]);
        }

    int[] nums = new int[higherVertices.Count];
    nums = higherVertices.ToArray();
        for (int i = 0; i < higherVertices.Count; i++)
        {
            newVertices[nums[i]] = new Vector3(newVertices[nums[i]].x, heightMap[nums[i]], newVertices[nums[i]].z);
        }

            mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.normals = newNormals.ToArray();
        mesh.RecalculateNormals();
        lowerVertices.Clear();
        higherVertices.Clear();
    }
}

    //private void OnColliderEnter(Collision collision)
    //{
    //    print("yo");
    //    if (collision.gameObject.CompareTag("bedViewpoint"))
    //    {
    //        Color color = GetComponent<Renderer>().material.color;
    //        color.a = 0f;

    //        GetComponent<Renderer>().material.SetColor("_Color", color);
     //   }
    //}
//}

