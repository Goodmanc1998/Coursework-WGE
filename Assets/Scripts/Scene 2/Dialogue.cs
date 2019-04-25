using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public GameObject speechPanel;

    public Text NPCText;
    public Text[] playerText;

    public string playerFinish;


    public PairSpeach[] pairSpeach;

    public XMLVoxelFileWriter XMLVoxelFileWriter;

    

    int[] branch;
    int[] branchplace;

    int position;

    bool[] talking;

    [System.Serializable]
    public class PairSpeach
    {

        public int previousOption;
        public int position;
        public string NPCSpeach;
        public string[] playerResponse;
        public int[] branch;

    }

    public GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        talking = new bool[3];
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            if (talking[0])
            {
                ConversationUpdate(1);
            }else
                ConversationUpdate(4);



        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            if (talking[1])
            {
                ConversationUpdate(2);
            }
            else
                ConversationUpdate(4);




        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            if (talking[2])
            {
                ConversationUpdate(3);
            }
            else
                ConversationUpdate(4);

        }


    }

    void ConversationStart()
    {

        speechPanel.SetActive(true);
        cameraMovement.ChangeTarget(false);

        position = 0;

        for (int i = 0; i < talking.Length; i++)
        {
            talking[i] = true;
        }

        NPCText.text = pairSpeach[0].NPCSpeach.ToString();

        for (int i = 0; i < pairSpeach[0].playerResponse.Length; i++)
        {
            playerText[i].text = pairSpeach[0].playerResponse[i].ToString();
        }

        player.GetComponent<PlayerController2D>().enabled = false;


    }

    void ConversationUpdate(int branch)
    {

        for (int i = 0; i < playerText.Length; i++)
            playerText[i].text = "";

        if (branch != 4)
        {
            if (playerText[branch - 1].ToString() != "")
            {
                position = position + 1;
                for (int i = 0; i < pairSpeach.Length; i++)
                {
                    if (pairSpeach[i].previousOption == branch && pairSpeach[i].position == position)
                    {
                        NPCText.text = pairSpeach[i].NPCSpeach.ToString();

                        for (int j = 0; j < pairSpeach[i].playerResponse.Length; j++)
                        {
                            playerText[j].text = pairSpeach[i].playerResponse[j].ToString();

                            if (playerText[j].text == playerFinish)
                            {
                                talking[j] = false;
                            }

                        }
                    }
                }
            }
            
        }

        


        if (branch == 4)
        {
            ConversationEnd();
        }

    }

    void ConversationEnd()
    {
        speechPanel.SetActive(false);
        cameraMovement.ChangeTarget(true);

        player.GetComponent<PlayerController2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            ConversationStart();

            
        }

        
    }
}
