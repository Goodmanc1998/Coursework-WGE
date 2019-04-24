using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public PlayerMovement2D playerMovement2D;

    Vector3 cameraPosition;
    Vector3 cameraCurrent;
    Vector3 playerPosition;

    Vector3 target;

    public GameObject player;

    int zposition = -10;
    bool cameraShakeB = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = gameObject.transform.position;

        cameraCurrent = gameObject.transform.position;

        //gameObject.transform.position = cameraCurrent;

        target = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);

        //if (cameraPosition != target)
        //StartCoroutine(CameraMovement());




        float t = 0;

        //float speed = 0.5f;

        while (t < 1)
        {
            t += Time.deltaTime;

            //Debug.Log("MOVING");

            gameObject.transform.position = cameraPosition + (target - cameraPosition) * speed * t;

            //gameObject.transform.position = Vector3.Lerp(cameraPosition, target, t);

            if (t >= 1)
            {
                gameObject.transform.position = target;
                t = 0;
            }
            t = 0;
            //yield return null;
        }



        if (Input.GetKey(KeyCode.PageDown))
            StartCoroutine(CameraShake(cameraCurrent));

        
    }

    public float speed = 2.5f;

    IEnumerator CameraMovement()
    {
        float t = 0;

        //float speed = 0.5f;

        while (t < 1)
        {
            t += Time.deltaTime;

            //Debug.Log("MOVING");

            gameObject.transform.position = cameraPosition + (target - cameraPosition) * speed * t;

            //gameObject.transform.position = Vector3.Lerp(cameraPosition, target, t);

            if (t >= 1)
            { 
                gameObject.transform.position = target;
            }
            yield return null;
        }
    }

    IEnumerator CameraShake(Vector3 cameraCurrent)
    {
        cameraShakeB = true;

        float shakeAmount = 10f;
        float timePassed = 0f;

        //Vector3 cameraShake; // = transform.position;

        Vector3 originalPos = transform.localPosition;

        while (timePassed < 0.5f)
        {

            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            //cameraShake = new Vector3(cameraCurrent.x + x, cameraCurrent.y + y, cameraCurrent.z);

            //transform.position = cameraShake;

            transform.position = new Vector3(x, y, originalPos.z);

            timePassed += Time.deltaTime;


            yield return null;
        }
        cameraShakeB = false;

        transform.localPosition = originalPos;

    }

}
