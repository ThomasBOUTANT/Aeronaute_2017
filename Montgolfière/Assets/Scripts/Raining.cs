using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raining : MonoBehaviour {

    private Vector3 new_position;
    [SerializeField]
    private float speed, scale;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        new_position = new Vector3(transform.position.x, transform.position.y, (transform.position.z - speed * Time.deltaTime) % scale);
        transform.position = new_position;
	}
}
