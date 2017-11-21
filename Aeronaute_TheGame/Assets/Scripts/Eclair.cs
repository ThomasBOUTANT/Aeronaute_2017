using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eclair : MonoBehaviour {

    [SerializeField]
    private float lifeTime;
    private float timer;

    [SerializeField]
    private GameObject background;

	// Use this for initialization
	void OnEnable () {
        timer = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Time.time - timer > lifeTime)
        {
            //Debug.Log("J'éteins l'éclair");
            background.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
