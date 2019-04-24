using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    InventoryManager inventoryManager;

    public GameObject inventory;

    public GameObject settingsPanel;
    public GameObject player;
    public GameObject sceneCamera;
    public string fileLoadName;
    bool panelVisible;

    // Start is called before the first frame update
    void Start()
    {
        sceneCamera.SetActive(true);
        player.SetActive(false);
        inventoryManager = inventory.gameObject.GetComponent<InventoryManager>();
    }

    public void GameStart()
    {
        sceneCamera.SetActive(false);
        player.SetActive(true);
        settingsPanel.SetActive(false);
        inventoryManager.InventoryActive();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.F3))
        {

        }


    }

    public void InputText(string newText)
    {
        fileLoadName = newText;
    }
}
