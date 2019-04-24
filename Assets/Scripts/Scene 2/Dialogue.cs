using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    public GameObject speechPanel;

    public Text NPCText;
    public Text[] playerText;

    public string NPCOpening;
    public string playerClosing;

    int branchLength;

    int[] branch;

    public string[] NPCSpeach;
    public string[] playerSpeach;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ConversationStart()
    {

    }

    void ConversationEnd()
    {

    }

    public void NewBranch(int branchL)
    {
        branch = new int[branchL];
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speechPanel.SetActive(true);
            ConversationStart();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speechPanel.SetActive(false);
        }
    }


}
