using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WeatherType {Rain,Storm,SandStorm};

public class Intemperie : MonoBehaviour {


    [SerializeField]
    private Damageables typeDamagables;
    [SerializeField]
    private float damages,delayDamages;

    [SerializeField]
    private int heightCoef;

    [SerializeField]
    private WeatherType type;


    private float timerDamages;
    private bool disabled,boosted;

    [SerializeField]
    private float baseSpeed;
    //[SerializeField]
    //type ? resourceDamanged;

    private float appearingDistance;

	// Use this for initialization
	void Start () {
        disabled = false;
        appearingDistance = transform.position.x;
        timerDamages = Time.time;
        boosted = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TouchPlayer(PlayerMovement player)
    {
        if (timerDamages < Time.time && !disabled)
        {
            if (boosted)
            {
                switch (heightCoef)
                {
                    case -1:
                        player.Damages(typeDamagables, -damages *2* (-player.GetHeight() + player.GetMaxPlayerZ()) / (player.GetMaxPlayerZ() - player.GetMinPlayerZ()));
                        break;


                    case 0:
                        player.Damages(typeDamagables, damages*2);
                        break;


                    case 1:
                        player.Damages(typeDamagables, 2*damages * (player.GetHeight() - player.GetMinPlayerZ()) / (player.GetMaxPlayerZ() - player.GetMinPlayerZ()));

                        break;
                }
            }
            else
            {
                switch (heightCoef)
                {
                    case -1:
                        player.Damages(typeDamagables, -damages * (-player.GetHeight() + player.GetMaxPlayerZ()) / (player.GetMaxPlayerZ() - player.GetMinPlayerZ()));
                        break;


                    case 0:
                        player.Damages(typeDamagables, damages);
                        break;


                    case 1:
                        player.Damages(typeDamagables, damages * (player.GetHeight() - player.GetMinPlayerZ()) / (player.GetMaxPlayerZ() - player.GetMinPlayerZ()));

                        break;
                }
            }
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

    public void Disable()
    {
        disabled = true;
    }

    public void Boost()
    {
        boosted = true;
    }

    public WeatherType GetWeatherType()
    {
        return type;
    }


}
