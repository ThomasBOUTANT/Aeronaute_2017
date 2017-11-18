using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour {

    //Liste d'intempéries connues à l'avance, scriptées
    [SerializeField]
    private GameObject[] intemperies;
    private int nb_intemperies;

    private float currentDistance;

    GameObject player;

    [SerializeField]
    private float margin;


    // Use this for initialization
    void Start () {
        nb_intemperies = intemperies.Length;
        player = GameObject.FindGameObjectWithTag("Player");
        currentDistance = 0;

        for (int j = 0; j < nb_intemperies; j++)
        {
            intemperies[j].SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        currentDistance = player.GetComponent<PlayerMovement>().GetCurrentDistance();

        CheckIntemperies();
		
	}

    int CheckIntemperies()
    {
        int i = -1;

        for(int j = 0; j < nb_intemperies; j++)
        {
            if (!intemperies[j].activeSelf
                && intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() < currentDistance + margin
                && intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() > currentDistance - margin
                )
            {
                //On active l'intempérie à l'endroit du joueur
                intemperies[j].SetActive(true);
                i = j;
                break;
            }
            else if (intemperies[j].activeSelf)
            {
                if (intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() > currentDistance + margin
                || intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() < currentDistance - margin)
                {
                    intemperies[j].SetActive(false);
                } else if(intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() < currentDistance + margin/2
                     && intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() > currentDistance - margin/2)
                {
                    player.GetComponent<PlayerMovement>().SetBaseSpeed(intemperies[j].GetComponent<Intemperie>().GetBaseSpeed());
                    intemperies[j].GetComponent<Intemperie>().TouchPlayer(player.GetComponent<PlayerMovement>());
                }
            }
            else
            {
                player.GetComponent<PlayerMovement>().SetBaseSpeed(25);
            }
        }
        return i;
    }


}
