using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    private float minCameraZ; //la camera ne va pas plus haut 
    private float maxCameraZ; // la camera ne va pas plus bas 

    [SerializeField]
    private float marginCameraZ;


    // Use this for initialization 
    void Start()
    {
        minCameraZ = player.GetComponent<PlayerMovement>().GetMinPlayerZ() + marginCameraZ;
        maxCameraZ = player.GetComponent<PlayerMovement>().GetMaxPlayerZ() - marginCameraZ;
    }

    void Update()
    {
        float newX = player.transform.position.x;
        float newZ = player.transform.position.z;
        float newY = transform.position.y;

        if (Input.GetButtonDown("Fire1"))
        {
            newY -= 10;
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            newY += 10;
        }

        if (newZ < minCameraZ || newZ > maxCameraZ)
        {
            transform.position = new Vector3(newX, newY, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(newX, newY, newZ);
        }
    }


}