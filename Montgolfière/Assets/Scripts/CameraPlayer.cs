using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour {

    [SerializeField]
    private GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float newX = player.transform.position.x;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
