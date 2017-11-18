using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour  {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float minZ; //le joueur ne va pas plus bas

    [SerializeField]
    private float maxZ; // le joueur ne va pas plus haut

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float newX = player.transform.position.x;
        float newZ = player.transform.position.z;

        if ( newZ < minZ || newZ > maxZ)
        {
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(newX, transform.position.y, newZ);
        }
	}
}
