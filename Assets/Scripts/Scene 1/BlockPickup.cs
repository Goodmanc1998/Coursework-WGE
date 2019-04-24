using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPickup : MonoBehaviour
{
    public int pickupAmount;
    InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        //playerScript = player.GetComponent<PlayerScript>;

    }

    private void Awake()
    {
        //inventoryManager = GetComponent<InventoryManager>();


        GameObject Inv = GameObject.Find("InventoryObject");
        inventoryManager = Inv.GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
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
