using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string secondScene;

    InventoryManager inventoryManager;

    public GameObject inventory;

    public GameObject settingsPanel;
    public GameObject player;
    public GameObject sceneCamera;
    public string fileLoadName;
    bool panelVisible;

    Vector3 playerStart;

    private void Awake()
    {
        //Storing the player start position
        playerStart = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneCamera.SetActive(true);
        player.SetActive(false);
        inventoryManager = inventory.gameObject.GetComponent<InventoryManager>();
        player.transform.position = playerStart;
    }

    public void GameStart()
    {
        sceneCamera.SetActive(false);
        player.SetActive(true);
        settingsPanel.SetActive(false);
        inventoryManager.InventoryActive(true);
    }

    public void Menu()
    {
        sceneCamera.SetActive(true);
        player.SetActive(false);

        settingsPanel.SetActive(true);
        inventoryManager.InventoryActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
        }
    }

    public void InputText(string newText)
    {
        fileLoadName = newText;
    }

    public void SceneTwo()
    {
        SceneManager.LoadScene(secondScene);
    }
}
