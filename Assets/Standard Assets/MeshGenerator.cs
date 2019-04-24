using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds a MeshFilter and MeshRendered component
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]

public class MeshGenerator : MonoBehaviour {

    Mesh mesh;

    MeshCollider meshCollider;

    List<Vector3> vertexList;
    List<int> triIndexList;
    List<Vector2> UVList;

    int numQuads = 0;

    // Use this for initialization
    void Start () {

        //RequireComponent declaration earlier should guarantee a MeshFilter component
        mesh = GetComponent<MeshFilter>().mesh;

        meshCollider = GetComponent<MeshCollider>();

        vertexList = new List<Vector3>();
        triIndexList = new List<int>();
        UVList = new List<Vector2>();

        CreateQuad(1,1, new Vector2(0, 0.5f));
        CreateQuad(2,1, new Vector2(0.5f, 0.5f));
        CreateQuad(3, 1, new Vector2(0.5f, 1f));
        CreateQuad(4, 1, new Vector2(0f, 0f));

        //Convert lists to arrays and store in mesh
        mesh.vertices = vertexList.ToArray();
        mesh.triangles = triIndexList.ToArray();

        //Convert UV list to array and store in mesh
        mesh.uv = UVList.ToArray();

        //Recalculating Normals to avoid Errors
        mesh.RecalculateNormals();

        //Setting up mesh coliders
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;

    }

    // Update is called once per frame
    void Update () {
		
	}

    void CreateQuad(int x, int y, Vector2 uvCoords)
    {

        //List of Vertices
        vertexList.Add(new Vector3(x, y + 1, 0));
        vertexList.Add(new Vector3(x + 1, y + 1, 0));
        vertexList.Add(new Vector3(x + 1, y, 0));
        vertexList.Add(new Vector3(x, y, 0));

        //List of Triangle Indices
        triIndexList.Add(numQuads * 4);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 3);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 2);
        triIndexList.Add((numQuads * 4) + 3);
        numQuads++;

        //Storing the texture
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y));
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y));




    }
}
