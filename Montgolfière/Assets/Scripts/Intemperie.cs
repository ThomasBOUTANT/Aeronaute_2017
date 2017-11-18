using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WeatherType { Rain,Thunder};

public class Intemperie : MonoBehaviour {

    [SerializeField]
    private float damages;

    [SerializeField]
    private WeatherType type;
    //[SerializeField]
    //type ? resourceDamanged;

    private float appearingDistance;

	// Use this for initialization
	void Start () {
        appearingDistance = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void touchPlayer()
    {

    }

    public float GetAppearingDistance()
    {
        return appearingDistance;
    }


}
