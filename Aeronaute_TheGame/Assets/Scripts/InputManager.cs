using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    [SerializeField]
    private float delayLest;
    private float timerLest;
	// Use this for initialization
	void Start () {
        timerLest = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        GetComponent<PlayerMovement>().Move(moveHorizontal);

        float moveVertical = Input.GetAxis("Vertical");
        GetComponent<PlayerMovement>().Burn(moveVertical);

        if (Input.GetButton("Fire3") && timerLest < Time.time)
        {
            GetComponent<PlayerMovement>().LacherLest();
            timerLest = Time.time + delayLest;
        }
    }
}
