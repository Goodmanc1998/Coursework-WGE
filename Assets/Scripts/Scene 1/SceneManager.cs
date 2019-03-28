using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    VoxelChunk voxelChunk;
    public GameObject UI;

    public string fileLoadName;

    bool invetory;



    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (invetory)
        {
            Cursor.visible = true;
        }
        else
            Cursor.visible = false;

        if(Input.GetKey(KeyCode.Space))
        {
            if(invetory == false)
            {
                invetory = true;
                UI.gameObject.SetActive(true);
            }
            else if(invetory)
            {
                invetory = false;
                UI.gameObject.SetActive(false);
            }
        }
    }

    public void InputText(string newText)
    {
        fileLoadName = newText;
    }

}
