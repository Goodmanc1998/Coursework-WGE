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


    // Start is called before the first frame update
    void Start()
    {

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

    public void InventoryActive()
    {
        mainPanel.SetActive(true);
    }

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
        if (Input.GetKey(KeyCode.F5))
            MergeSort(inventoryList);
            //StartQuickSort();

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemNameText.text == "Grass")
            {
                inventoryList[i].itemAmountText.text = itemAmounts[0].ToString();
            }
            if (inventoryList[i].itemNameText.text == "Dirt")
            {
                inventoryList[i].itemAmountText.text = itemAmounts[1].ToString();
            }
            if (inventoryList[i].itemNameText.text == "Sand")
            {
                inventoryList[i].itemAmountText.text = itemAmounts[2].ToString();
            }
            if (inventoryList[i].itemNameText.text == "Stone")
            {
                inventoryList[i].itemAmountText.text = itemAmounts[3].ToString();
            }
        }

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


    public void SelectionSortInventory()
    {
        // iterate through every item in the list except last
        for (int i = 0; i < inventoryList.Count - 1; i++)
        {
            int minIndex = i;
            // iterate through unsorted portion of the list
            for (int j = i; j < inventoryList.Count; j++)
            {
                if (inventoryList[j].itemAmount <
                inventoryList[minIndex].itemAmount)
                {
                    minIndex = j;
                }
            }
            // Swap the minimum item into position
            if (minIndex != i)
            {
                InventoryItemScript iis = inventoryList[i];
                inventoryList[i] = inventoryList[minIndex];
                inventoryList[minIndex] = iis;
            }
        }
        // Display the list in the new correct order
        DisplayListInOrder();
    }

    public void StartQuickSort()
    {

        inventoryList = QuickSort(inventoryList);
        DisplayListInOrder();
    }

    List<InventoryItemScript> QuickSort(List<InventoryItemScript> listIn)
    {
        if (listIn.Count <= 1)
        {
            return listIn;
        }

        // Naive pivot selection
        int pivotIndex = 0;

        // Left and right lists
        List<InventoryItemScript> leftList = new List<InventoryItemScript>();
        List<InventoryItemScript> rightList = new List<InventoryItemScript>();

        for (int i = 1; i < listIn.Count; i++)
        {
            // Compare amounts to determine list to add to
            if (listIn[i].itemAmount > listIn[pivotIndex].itemAmount)
            {
                // Greater than pivot to left list
                leftList.Add(listIn[i]);
            }
            else
            {
                // Smaller than pivot to right list
                rightList.Add(listIn[i]);
            }
        }

        // Recurse left list
        leftList = QuickSort(leftList);

        //Recurse right list
        rightList = QuickSort(rightList);

        // Concatenate lists: left + pivot + right
        leftList.Add(listIn[pivotIndex]);
        leftList.AddRange(rightList);
        return leftList;
    }

    public void StartMergeSort()
    {

        inventoryList = MergeSort(inventoryList);
        DisplayListInOrder();
    }

    List<InventoryItemScript> MergeSort(List<InventoryItemScript> listIn)
    {

        if (listIn.Count <= 1)
            return listIn;


        List<InventoryItemScript> left = new List<InventoryItemScript>();
        List<InventoryItemScript> right = new List<InventoryItemScript>();

        int middle = listIn.Count / 2;

        for (int i = 0; i < middle; i++)
        {
            left.Add(listIn[i]);
            right.Add(listIn[middle + i]);
        }

        MergeSort(left);
        MergeSort(right);

        return listIn = Merge(left, right);

    }

    List<InventoryItemScript> Merge(List<InventoryItemScript> leftList, List<InventoryItemScript> rightList)
    {
        List<InventoryItemScript> merged = new List<InventoryItemScript>();
        int i = 0;
        int j = 0;
        /*
        while (i < leftList.Count || j < rightList.Count)
        {
            if(leftList[i].itemAmount <= rightList[j].itemAmount)
            {
                merged.Add(leftList[i]);
                leftList.Remove(leftList[i]);
                i++;
            } else
            {
                merged.Add(rightList[j]);
                rightList.Remove(rightList[i]);
                j++;
            }
        }

        if (i < leftList.Count)
        {
            merged.Add(leftList[i]);
            leftList.Remove(leftList[i]);
        }
        else if (j < rightList.Count)
        {
            merged.Add(rightList[j]);
            rightList.Remove(rightList[i]);
        }*/

        while (leftList.Count > 0 || rightList.Count > 0)
        {
            if (leftList.Count > 0 && rightList.Count > 0)
            {

                if (leftList[0].itemAmount <= rightList[0].itemAmount)
                {
                    merged.Add(leftList[0]);
                    leftList.Remove(leftList[0]);
                    i++;
                }
                else
                {
                    merged.Add(rightList[0]);
                    rightList.Remove(rightList[0]);
                    j++;
                }
            } else if (leftList.Count > 0)
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


        return merged;
    }

}
