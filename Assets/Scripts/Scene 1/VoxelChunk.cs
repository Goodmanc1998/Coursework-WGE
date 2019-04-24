﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelChunk : MonoBehaviour {

    public SceneManager sceneManager;
    PlayerScript playerScript;

    VoxelGenerator voxelGenerator;
    int[,,] terrainArray;
    int chunkSize = 16;

    // delegate signature
    public delegate void EventBlockChanged();

    // event instances for EventBlockChanged
    public static event EventBlockChanged OnEventBlockDestroyed;
    public static event EventBlockChanged OnEventBlockPlaced;

    public static event EventBlockChanged OnEventGrassDestroy;
    public static event EventBlockChanged OnEventGrassPlaced;

    public static event EventBlockChanged OnEventDirtDestroy;
    public static event EventBlockChanged OnEventDirtPlaced;

    public static event EventBlockChanged OnEventStoneDestroy;
    public static event EventBlockChanged OnEventStonePlaced;

    public static event EventBlockChanged OnEventSandDestroy;
    public static event EventBlockChanged OnEventSandPlaced;



    public GameObject Grass;
    public GameObject Dirt;
    public GameObject Stone;
    public GameObject Sand;

    public void Start()
    {

        voxelGenerator = GetComponent<VoxelGenerator>();
        //Instantiate the arrray with size based n chunk size
        terrainArray = new int[chunkSize, chunkSize, chunkSize];

        voxelGenerator.Initialise();

        InitialiseTerrain();
        CreateTerrain();

        voxelGenerator.updateMesh();

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            // Get terrainArray from XML file
            terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, sceneManager.fileLoadName);
            // Draw the correct faces
            CreateTerrain();
            // Update mesh info
            voxelGenerator.updateMesh();

        }
    }

    public void Apply()
    {
        // Get terrainArray from XML file
        terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, sceneManager.fileLoadName);
        // Draw the correct faces
        CreateTerrain();
        // Update mesh info
        voxelGenerator.updateMesh();

        sceneManager.GameStart();
    }

    public void Save()
    {
        XMLVoxelFileWriter.SaveChunkToXMLFile(terrainArray, sceneManager.fileLoadName);
    }

     void InitialiseTerrain()
     {
        //Iterate horizontal on width
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            // iterate vertically
            for (int y = 0; y < terrainArray.GetLength(0); y++)
            {
                //iterate per vovel horizontally on depth
                for (int z = 0; z < terrainArray.GetLength(0); z++)
                {
                    
                    //if we are operating on 4th layer
                    if (y == 3)
                    {

                        terrainArray[x, y, z] = 1;
                        /*
                        terrainArray[0, 3, 1] = 4;
                        terrainArray[0, 3, 2] = 4;
                        terrainArray[0, 3, 3] = 4;
                        terrainArray[1, 3, 3] = 4;
                        terrainArray[1, 3, 4] = 4;
                        terrainArray[2, 3, 4] = 4;
                        terrainArray[3, 3, 4] = 4;
                        terrainArray[4, 3, 4] = 4;
                        terrainArray[5, 3, 4] = 4;
                        terrainArray[5, 3, 3] = 4;
                        terrainArray[5, 3, 2] = 4;
                        terrainArray[6, 3, 2] = 4;
                        terrainArray[7, 3, 2] = 4;
                        terrainArray[8, 3, 2] = 4;
                        terrainArray[9, 3, 2] = 4;
                        terrainArray[10, 3, 2] = 4;
                        terrainArray[11, 3, 2] = 4;
                        terrainArray[12, 3, 2] = 4;
                        terrainArray[13, 3, 2] = 4;
                        terrainArray[13, 3, 3] = 4;
                        terrainArray[14, 3, 3] = 4;
                        terrainArray[15, 3, 3] = 4;
                        */

                    }
                    //else if the layer is below the forth
                    else if (y < 3)
                    {
                        terrainArray[x, y, z] = 2;
                    }
                }
            }
        }
    }

    void CreateTerrain()
    {
        //iterate horizontally on width
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            //iterate vertically
            for (int y = 0; y < terrainArray.GetLength(1); y++)
            {
                //iterate per voxel horizontally on depth
                for (int z = 0; z < terrainArray.GetLength(2); z++)
                {
                    //if this voxel is not empty
                    if (terrainArray[x, y, z] != 0)
                    {
                        string tex;
                        //set texture name by value
                        switch (terrainArray[x, y, z])
                        {
                            case 1:
                                tex = "Grass";
                                break;
                            case 2:
                                tex = "Dirt";
                                break;
                            case 3:
                                tex = "Sand";
                                break;
                            case 4:
                                tex = "Stone";
                                break;
                            default:
                                tex = "Grass";
                                break;

                        }
                        //voxelGenerator.CreateVoxel(x, y, z, tex);

                        //check if we need to draw the negative x face
                        if (x == 0 || terrainArray[x - 1, y, z] == 0)
                        {
                            voxelGenerator.CreateNegativeXFace(x, y, z, tex);
                        }
                        //Check if we need to draw the positive x face
                        if (x == terrainArray.GetLength(0) - 1 || terrainArray[x + 1, y, z] == 0)
                        {
                            voxelGenerator.CreatePositiveXFace(x, y, z, tex);
                        }

                        //check if we need to draw the negative y face
                        if (y == 0 || terrainArray[x, y - 1, z] == 0)
                        {
                            voxelGenerator.CreateNegativeYFace(x, y, z, tex);
                        }
                        //Check if we need to draw the positive y face
                        if (y == terrainArray.GetLength(0) - 1 || terrainArray[x, y + 1, z] == 0)
                        {
                            voxelGenerator.CreatePositiveYFace(x, y, z, tex);
                        }

                        //check if we need to draw the negative z face
                        if (z == 0 || terrainArray[x, y, z - 1] == 0)
                        {
                            voxelGenerator.CreateNegativeZFace(x, y, z, tex);
                        }
                        //Check if we need to draw the positive z face
                        if (z == terrainArray.GetLength(0) - 1 || terrainArray[x, y, z + 1] == 0)
                        {
                            voxelGenerator.CreatePositiveZFace(x, y, z, tex);
                        }
                        //print("Create " + tex + " block");
                    }
                }
            }
        }
    }

    public void SetBlock(Vector3 index, int blockType)
    {
        if ((index.x > -1 && index.x < terrainArray.GetLength(0)) && (index.y > -1 && index.y < terrainArray.GetLength(1)) && (index.z > -1 && index.z < terrainArray.GetLength(2)))
        {

            //Change the block to the required type
            terrainArray[(int)index.x, (int)index.y, (int)index.z] = blockType;

            //Create the new mesh
            CreateTerrain();

            //Update the mesh data
            voxelGenerator.updateMesh();

            //Debug.Log(blockType);
        }

        //Debug.Log(blockType);

        if (blockType == 1)
        {
            OnEventGrassPlaced();
        }
        else if(blockType == 2)
        {
            OnEventDirtPlaced();
        }
        else if(blockType == 3)
        {
            OnEventSandPlaced();
        }
        else if(blockType == 4)
        {
            OnEventStonePlaced();
        }
        
    }

    public void SpawnSmallBlock(Vector3 index)
    {
        Vector3 spawnPosition;
        spawnPosition.x = index.x; ;
        spawnPosition.y = index.y + 1;
        spawnPosition.z = index.z;

        int destroyedBlock = terrainArray[(int)index.x, (int)index.y, (int)index.z];

        Debug.Log(destroyedBlock);

        GameObject smallBlock = null;
        switch (destroyedBlock)
        {
            case 1:
                smallBlock = Grass;
                break;
            case 2:
                smallBlock = Dirt;
                break;
            case 3:
                smallBlock = Sand;
                break;
            case 4:
                smallBlock = Stone;
                break;
            default:
                smallBlock = Grass;
                break;
        }

        GameObject block = Instantiate(smallBlock, spawnPosition, transform.rotation) as GameObject;

        if(destroyedBlock == 1)
        {
            OnEventGrassDestroy();
        }
        else if(destroyedBlock == 2)
        {
            OnEventDirtDestroy();
        }
        else if(destroyedBlock == 3)
        {
            OnEventSandDestroy();
        }
        else if(destroyedBlock == 4)
        {
            OnEventStoneDestroy();
        }
        
    }
}