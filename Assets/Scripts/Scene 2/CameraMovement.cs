using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
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
        if(followPlayer)
        {
            targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, cameraPosition.z);
        }
        else
        {
            targetPosition = new Vector3(NPC.transform.position.x, NPC.transform.position.y, cameraPosition.z);
        }


        transform.position = Vector3.Lerp(cameraPosition, targetPosition, speed);

        if(shakeTimer >= 0)
        {
            Vector2 cameraShake = Random.insideUnitCircle * shakePower;

            transform.position = new Vector3(transform.position.x + cameraShake.x, transform.position.y + cameraShake.y, transform.position.z);

            shakeTimer -= Time.deltaTime;

        }


        
    }

    public void CameraShake()
    {
        shakeTimer = shakeLength;
    }

    public void ChangeTarget(bool NPCCollided)
    {
        followPlayer = NPCCollided;
    }

}
