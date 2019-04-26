using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public PlayerScript playerScript;

    // Parent object inventory item
    public Transform parentPanel;
    public GameObject mainPanel;

    // Item info to build inventory items
    public List<Sprite> itemSprites;
    public List<string> itemNames;
    public List<int> itemAmounts;

    // Starting template item
    public GameObject startItem;

    List<InventoryItemScript> inventoryList;
    List<InventoryItemScript> sortingList;

    bool lowestFirst;


    // Start is called before the first frame update
    void Start()
    {
        lowestFirst = true;

        inventoryList = new List<InventoryItemScript>();
        for (int i = 0; i < itemNames.Count; i++)
        {
            // Create a duplicate of the starter item
            GameObject inventoryItem = (GameObject)Instantiate(startItem);
            // UI items need to parented by the canvas or an object within the canvas
            inventoryItem.transform.SetParent(parentPanel);

            // Original start item is disabled – so the duplicate must be enabled
            inventoryItem.SetActive(true);

            // Get InventoryItemScript component so we can set the data
            InventoryItemScript iis = inventoryItem.GetComponent<InventoryItemScript>();
            iis.itemSprite.sprite = itemSprites[i];
            iis.itemNameText.text = itemNames[i];
            iis.itemName = itemNames[i];
            iis.itemAmountText.text = itemAmounts[i].ToString();
            iis.itemAmount = itemAmounts[i];

            // Keep a list of the inventory items
            inventoryList.Add(iis);
        }

        DisplayListInOrder();

        mainPanel.SetActive(false);

    }

    //Method to be called to enable panel
    public void InventoryActive(bool active)
    {
        mainPanel.SetActive(active);
    }

    //Method to be called to check the name of the active item slot to update correct amount
    public void InventorySlot(int place)
    {
        
        if (inventoryList[place].itemName == "Grass")
        {
            playerScript.blockType = 1;
        }
        else if (inventoryList[place].itemName == "Dirt")
        {
            playerScript.blockType = 2;
        }
        else if (inventoryList[place].itemName == "Sand")
        {
            playerScript.blockType = 3;
        }
        else if (inventoryList[place].itemName == "Stone")
        {
            playerScript.blockType = 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Updating the text elements in the inventoryList to the correct name and string
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemNameText.text == "Grass")
            {
                inventoryList[i].itemAmountText.text = itemAmounts[0].ToString();
                inventoryList[i].itemAmount = itemAmounts[0];
            }
            if (inventoryList[i].itemNameText.text == "Dirt")
            {
                inventoryList[i].itemAmountText.text = itemAmounts[1].ToString();
                inventoryList[i].itemAmount = itemAmounts[1];
            }
            if (inventoryList[i].itemNameText.text == "Sand")
            {
                inventoryList[i].itemAmountText.text = itemAmounts[2].ToString();
                inventoryList[i].itemAmount = itemAmounts[2];
            }
            if (inventoryList[i].itemNameText.text == "Stone")
            {
                inventoryList[i].itemAmountText.text = itemAmounts[3].ToString();
                inventoryList[i].itemAmount = itemAmounts[3];
            }
        }

        //Checking if the player presses the key to run StartMergeSort
        if (Input.GetKeyDown(KeyCode.F5))
        {
            StartMergeSort();
        }

        //Changing sort from high to low or low to high
        if (Input.GetKeyDown(KeyCode.Alpha0))
            lowestFirst = false;
        if (Input.GetKeyDown(KeyCode.Alpha9))
            lowestFirst = true;

    }

    void DisplayListInOrder()
    {
        // Height of item plus space between each
        float xOffset = 70f;

        // Use the start position for the first item
        Vector3 startPosition = startItem.transform.position;

        foreach (InventoryItemScript iis in inventoryList)
        {
            iis.transform.position = startPosition;
            //set position of next item using offset
            startPosition.x += xOffset;
        }
    }

    //Method to start MergeSort and to display the list in order
    public void StartMergeSort()
    {
        inventoryList = MergeSort(inventoryList);

        DisplayListInOrder();
    }

    List<InventoryItemScript> MergeSort(List<InventoryItemScript> listIn)
    {
        //Checking if the list is 1 or less as the method wouldnt need to run
        if (listIn.Count <= 1)
            return listIn;

        //creating new lists
        List<InventoryItemScript> left = new List<InventoryItemScript>();
        List<InventoryItemScript> right = new List<InventoryItemScript>();

        //Finding the middle
        int middle = listIn.Count / 2;

        //Setting up the left and right list
        for (int i = 0; i < middle; i++)
        {
            left.Add(listIn[i]);
            right.Add(listIn[middle + i]);
        }

        //Recursive call
        left = MergeSort(left);
        right = MergeSort(right);

        //Passing left and right lists to Merge
        return listIn = Merge(left, right);

    }

    List<InventoryItemScript> Merge(List<InventoryItemScript> leftList, List<InventoryItemScript> rightList)
    {
        List<InventoryItemScript> merged = new List<InventoryItemScript>();

        //While one of the list contains > 0
        while (leftList.Count > 0 || rightList.Count > 0)
        {
            //If both the lists are > 0
            if (leftList.Count > 0 && rightList.Count > 0)
            {
                //Check for lowest first
                if (lowestFirst)
                {
                    //Checking the first elements for the lowest then adding to merged and removing from left or right
                    if (leftList[0].itemAmount <= rightList[0].itemAmount)
                    {
                        merged.Add(leftList[0]);
                        leftList.Remove(leftList[0]);
                    }
                    else
                    {
                        merged.Add(rightList[0]);
                        rightList.Remove(rightList[0]);
                    }
                }
                else
                {
                    if (leftList[0].itemAmount >= rightList[0].itemAmount)
                    {
                        merged.Add(leftList[0]);
                        leftList.Remove(leftList[0]);
                    }
                    else
                    {
                        merged.Add(rightList[0]);
                        rightList.Remove(rightList[0]);
                    }
                }
            }
            //Adding rest of elements
            else if (leftList.Count > 0)
            {
                merged.Add(leftList[0]);
                leftList.Remove(leftList[0]);
            }
            else if (rightList.Count > 0)
            {
                merged.Add(rightList[0]);
                rightList.Remove(rightList[0]);
            }
        }
        //returning the merged list
        return merged;
    }
}
