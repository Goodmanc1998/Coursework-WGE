using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    //storing required scripts and variables
    public VoxelChunk voxelChunk;
    public InventoryManager inventoryManager;

    public bool destroying;
    public int blockType;

    string activeBlock;

    // Use this for initialization
    void Start()
    {
        blockType = 1;
    }

    VoxelGenerator voxelGenerator;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F3))
        {
            voxelChunk.Save();
        }


        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 v;
            destroying = true;

            if (pickingBlock(out v, 4))
            {
                voxelChunk.SpawnSmallBlock(v);
                voxelChunk.SetBlock(v, 0);
                

            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Vector3 v;
            destroying = false;

            if (blockType == 1)
            {
                if (inventoryManager.itemAmounts[0] > 0)
                {
                    inventoryManager.itemAmounts[0]--;

                    if (pickingBlock(out v, 4))
                    {
                        voxelChunk.SetBlock(v, blockType);
                    }
                }
            }
            if (blockType == 2)
            {
                if (inventoryManager.itemAmounts[1] > 0)
                {
                    inventoryManager.itemAmounts[1]--;

                    if (pickingBlock(out v, 4))
                    {
                        voxelChunk.SetBlock(v, blockType);
                    }
                }
            }
            if (blockType == 3)
            {
                if (inventoryManager.itemAmounts[2] > 0)
                {
                    inventoryManager.itemAmounts[2]--;

                    if (pickingBlock(out v, 4))
                    {
                        voxelChunk.SetBlock(v, blockType);
                    }
                }
            }
            if (blockType == 4)
            {
                if (inventoryManager.itemAmounts[3] > 0)
                {
                    inventoryManager.itemAmounts[3]--;

                    if (pickingBlock(out v, 4))
                    {
                        voxelChunk.SetBlock(v, blockType);
                    }
                }
            }


        }


        if (Input.GetKey(KeyCode.Alpha1))
        {
            //blockType = 1;
            inventoryManager.InventorySlot(0);
        }

        if (Input.GetKey(KeyCode.Alpha2))
            inventoryManager.InventorySlot(1);
        if (Input.GetKey(KeyCode.Alpha3))
            inventoryManager.InventorySlot(2);
        if (Input.GetKey(KeyCode.Alpha4))
            inventoryManager.InventorySlot(3);




    }

    public bool pickingBlock(out Vector3 v, float dist)
    {
        v = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist))
        {
            //Offset towards the centre of the block hit
            if (destroying)
                v = hit.point - hit.normal / 2;
            else if (destroying == false)
                v = hit.point + hit.normal / 2;

            //Round down to get the index of the block hit
            v.x = Mathf.Floor(v.x);
            v.y = Mathf.Floor(v.y);
            v.z = Mathf.Floor(v.z);
            return true;
        }
        return false;
    }

    
}
