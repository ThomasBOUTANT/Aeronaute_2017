using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : Intemperie {

    [SerializeField]
    private GameObject[] lights;
    [SerializeField]
    private GameObject background;

    [SerializeField]
    private float lightsRate;
    private float timer;
    private int i = 0;

	// Use this for initialization
	void Start () {
        timer = Time.time;
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Time.time - timer > lightsRate)
        {
            timer = Time.time;
            lights[i].SetActive(true);
            background.SetActive(false);

            i++;
            if(i > lights.Length - 1)
            {
                i = 0;
            }
        }
	}
}
