using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        GetComponent<PlayerMovement>().Move(moveHorizontal);

        float moveVertical = Input.GetAxis("Vertical");
        GetComponent<PlayerMovement>().Burn(moveVertical);
    }
}
