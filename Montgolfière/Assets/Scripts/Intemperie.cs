using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WeatherType {Wind,Rain,Bruine,Snow,Grele,Myst,Storm,SandStorm,AshStorm};

public class Intemperie : MonoBehaviour {


    [SerializeField]
    private Damageables typeDamagables;
    [SerializeField]
    private float damages,delayDamages;

    [SerializeField]
    private WeatherType type;


    private float timerDamages;

    [SerializeField]
    private float baseSpeed;
    //[SerializeField]
    //type ? resourceDamanged;

    private float appearingDistance;

	// Use this for initialization
	void Start () {
        appearingDistance = transform.position.x;
        timerDamages = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TouchPlayer(PlayerMovement player)
    {
        if (timerDamages < Time.time)
        {
            player.Damages(typeDamagables, damages);
            timerDamages = Time.time + delayDamages;
        }

    }

    public float GetAppearingDistance()
    {
        return appearingDistance;
    }

    public float GetBaseSpeed()
    {
        return baseSpeed;
    }


}
