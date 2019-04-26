using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //storing required GameObjects and variables
    public GameObject player;
    public GameObject NPC;

    public bool followPlayer;

    Vector3 targetPosition;
    Vector3 cameraPosition;

    public float speed;

    public float shakeTimer;
    public float shakePower;
    public float shakeLength;


    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = transform.position;
        //Checking if the camera needs to focus on the player or the NPC
        if(followPlayer)
        {
            targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, cameraPosition.z);
        }
        else
        {
            targetPosition = new Vector3(NPC.transform.position.x, NPC.transform.position.y, cameraPosition.z);
        }

        //Moving the camera to the target position
        transform.position = Vector3.Lerp(cameraPosition, targetPosition, speed);

        //Checking if shake timer is greater than 0
        if(shakeTimer >= 0)
        {
            //getting random Vector2 within a radius of x = 1 to x = -1 and same with y
            Vector2 cameraShake = Random.insideUnitCircle * shakePower;
            //applying the Vector2 to the Target
            transform.position = new Vector3(transform.position.x + cameraShake.x, transform.position.y + cameraShake.y, transform.position.z);
            //Updating timer
            shakeTimer -= Time.deltaTime;

        }
    }

    //Method to be called to start CameraShake
    public void CameraShake()
    {
        shakeTimer = shakeLength;
    }
    //Method to be called to change target
    public void ChangeTarget(bool NPCCollided)
    {
        followPlayer = NPCCollided;
    }

}
