using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPickup : MonoBehaviour
{
    //Storing required scripts and variables
    public int pickupAmount;
    InventoryManager inventoryManager;

    private void Awake()
    {
        //Locating InventoryManager
        GameObject Inv = GameObject.Find("InventoryObject");
        inventoryManager = Inv.GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Checking if collision with player then checking the name to apply ammount to needed itemAmount
        if (collision.gameObject.tag == "Player")
        {
            if(gameObject.name == "Grass(Clone)")
            {
                inventoryManager.itemAmounts[0] += pickupAmount;
            }
            if (gameObject.name == "Dirt(Clone)")
            {
                inventoryManager.itemAmounts[1] += pickupAmount;
            }
            if (gameObject.name == "Sand(Clone)")
            {
                inventoryManager.itemAmounts[2] += pickupAmount;
            }
            if (gameObject.name == "Stone(Clone)")
            {
                inventoryManager.itemAmounts[3] += pickupAmount;
            }

            Destroy(this.gameObject);
        }

    }
}
