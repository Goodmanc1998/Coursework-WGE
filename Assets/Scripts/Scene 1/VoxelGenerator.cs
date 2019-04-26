using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class VoxelGenerator : MonoBehaviour {

    Mesh mesh;
    MeshCollider meshCollider;
    List<Vector3> vertexList;
    List<int> triIndexList;
    List<Vector2> UVList;

    int numQuads = 0;

    //Storing Textures
    public List<string> texNames;
    public List<Vector2> texCoords;

    //Amount of textures stored
    public float texSize;

    Dictionary<string, Vector2> texNameCoodDictionary;

    public void Initialise()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        vertexList = new List<Vector3>();
        triIndexList = new List<int>();
        UVList = new List<Vector2>();

        CreateTextureNameCoordDictionary();

        CreateVoxel(0, 0, 0, "Grass");

    }

    public void updateMesh()
    {

        mesh.Clear();

        //Convert index list to array and store in mesh
        mesh.vertices = vertexList.ToArray(); 

        //Convert index list to array and store in mesh
        mesh.triangles = triIndexList.ToArray();

        //Convert UV list to array and store in mesh
        mesh.uv = UVList.ToArray();
        mesh.RecalculateNormals();

        //Create a collision mesh
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;

        ClearPreviousData();

    }

    //CLear previous data structure use to create the mesh
    public void ClearPreviousData()
    {
        vertexList.Clear();
        triIndexList.Clear();
        UVList.Clear();
        numQuads = 0;
    }

    public void CreateVoxel(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoodDictionary[texture];

        CreateNegativeXFace(x, y, z, texture);
        CreatePositiveXFace(x, y, z, texture);

        CreateNegativeYFace(x, y, z, texture);
        CreatePositiveYFace(x, y, z, texture);

        CreateNegativeZFace(x, y, z, texture);
        CreatePositiveZFace(x, y, z, texture);
    }

    public void AddTrianglesIndices()
    {
        triIndexList.Add(numQuads * 4);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 3);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 2);
        triIndexList.Add((numQuads * 4) + 3);
        numQuads++;
    }

    public void AddUVCoords(Vector2 uvCoords)
    {
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y));
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y));

    }

    public void CreateNegativeZFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoodDictionary[texture];

        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x, y, z));

        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeZFace(int x, int y, int z, Vector2 uvCoords)
    {
        //List of Vertices
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x, y, z));

        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveZFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoodDictionary[texture];

        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveZFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeXFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoodDictionary[texture];

        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeXFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveXFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoodDictionary[texture];

        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveXFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeYFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoodDictionary[texture];

        vertexList.Add(new Vector3(x, y, z));
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeYFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y, z));
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveYFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoodDictionary[texture];

        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveYFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        AddTrianglesIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateTextureNameCoordDictionary()
    {
        //Create a dictionary instance before using
        texNameCoodDictionary = new Dictionary<string, Vector2>();

        //Check the number of names and coordinates match
        if(texNames.Count == texCoords.Count)
        {
            //Iterate through both lists
            for (int i = 0; i < texNames.Count; i++)
            {
                //Add the pairing to the dictionary
                texNameCoodDictionary.Add(texNames[i], texCoords[i]);
            }
        }
        else
        {
            //List counts are not matching
            Debug.Log("texNames and tex Coords count mismatch");
        }
    }
}
