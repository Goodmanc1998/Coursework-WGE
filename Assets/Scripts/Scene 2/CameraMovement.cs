using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    Vector3 targetPosition;
    Vector3 cameraPosition;

    public float speed;

    public float shakeTimer;
    public float shakePower;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = transform.position;

        targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, cameraPosition.z);

        transform.position = Vector3.Lerp(cameraPosition, targetPosition, speed);

        if(shakeTimer >= 0)
        {
            Vector2 cameraShake = Random.insideUnitCircle * shakePower;

            transform.position = new Vector3(transform.position.x + cameraShake.x, transform.position.y + cameraShake.y, transform.position.z);

            shakeTimer -= Time.deltaTime;

        }

        if(Input.GetButtonDown("Fire1"))
        {
            CameraShake(1);
        }

        
    }

    IEnumerator Movement()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;

            //gameObject.transform.position = cameraPosition + (targetPosition - cameraPosition) * t;

            //gameObject.transform.position = Vector3.Lerp(cameraPosition, targetPosition, speed);

            transform.position = Vector3.Lerp(cameraPosition, targetPosition, t);

            yield return null;

        }
    }

    public void CameraShake(float timeInAir)
    {
        shakeTimer = timeInAir;
    }
}
