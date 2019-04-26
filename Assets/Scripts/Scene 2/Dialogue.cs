using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    //Storing the required variables and scripts
    public CameraMovement cameraMovement;
    public GameObject speechPanel;
    public GameObject player;

    public Text NPCText;
    public Text[] playerText;

    [Tooltip("Enter what you would like the player to say to finish conversation")]
    public string playerFinish;

    int position;
    bool[] talking;

    [Tooltip("The amount of interactions")]
    public PairSpeach[] pairSpeach;

    [System.Serializable]
    public class PairSpeach
    {
        [Tooltip("The previous option selected (if first speach Previous option = 0)")]
        public int previousOption;
        [Tooltip("The position of this speach")]
        public int position;
        [Tooltip("What you want the NPC to say")]
        public string NPCSpeach;
        [Tooltip("Set to 3, Then enter what you would like the player to say")]
        public string[] playerResponse;
        [Tooltip("Set length to 3, then 1,2,3")]
        public int[] branch;

    }

    // Start is called before the first frame update
    void Start()
    {
        talking = new bool[3];
    }

    // Update is called once per frame
    void Update()
    {

        //Checking which button is being pressed to let ConversationUpdate know the branch selected
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (talking[0])
            {
                ConversationUpdate(1);
            }else if(playerText[0].text == "" || talking[0] == false)
                ConversationUpdate(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (talking[1])
            {
                ConversationUpdate(2);
            }
            else if(playerText[1].text == "" || talking[1] == false)
                ConversationUpdate(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (talking[2])
            {
                ConversationUpdate(3);
            }
            else if (playerText[2].text == "" || talking[2] == false)
                ConversationUpdate(4);
        }

    }

    //Method used to start the conversation
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

    //Method used to update the conversation
    void ConversationUpdate(int branch)
    {
        //Resetting all the player text
        for (int i = 0; i < playerText.Length; i++)
            playerText[i].text = "";

        //Checking if the branch is the closing branch
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
        //Checking the branch and if it equals the finishing branch then conversation end is called
        if (branch == 4)
        {
            ConversationEnd();
        }
    }

    //Method used to finish the conversation
    void ConversationEnd()
    {
        speechPanel.SetActive(false);
        cameraMovement.ChangeTarget(true);

        player.GetComponent<PlayerController2D>().enabled = true;
    }

    //Method used OnTriggerEnter to check if the player collides with the NPC if so then ConversationStart is called
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ConversationStart();
        }
    }
}
