using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class WeatherManager : MonoBehaviour
{



    //Liste d'intempéries connues à l'avance, scriptées

    [SerializeField]

    private GameObject[] intemperies;

    private int nb_intemperies;



    [SerializeField]

    private bool[] boosted;



    private float currentDistance;



    GameObject player;



    [SerializeField]

    private float margin;



    private AudioSource audiosourcePlayer;





    // Use this for initialization

    void Start()
    {

        nb_intemperies = intemperies.Length;

        player = GameObject.FindGameObjectWithTag("Player");

        currentDistance = 0;



        for (int j = 0; j < nb_intemperies; j++)

        {

            intemperies[j].SetActive(false);

        }

        audiosourcePlayer = player.GetComponent<AudioSource>();





    }



    // Update is called once per frame

    void Update()
    {

        if (!player)

        {

            player = GameObject.FindGameObjectWithTag("Player");

        }



        currentDistance = player.GetComponent<PlayerMovement>().GetCurrentDistance();



        if (CheckIntemperies() == -1) //s'il n'y a pas d'intemperies

        {

            audiosourcePlayer.mute = false;

        }



    }



    int CheckIntemperies()

    {

        int i = -1;



        for (int j = 0; j < nb_intemperies; j++)

        {

            if (!intemperies[j].activeSelf

                && intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() < currentDistance + margin

                && intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() > currentDistance - margin

                )

            {

                //On active l'intempérie à l'endroit du joueur

                intemperies[j].SetActive(true);

                if (boosted[(int)intemperies[j].GetComponent<Intemperie>().GetWeatherType()])

                {

                    intemperies[j].GetComponent<Intemperie>().Boost();

                }

                i = j;

                break;

            }

            else if (intemperies[j].activeSelf)

            {

                if (intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() > currentDistance + margin

                || intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() < currentDistance - margin)

                {

                    intemperies[j].SetActive(false);

                }

                else if (intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() < currentDistance + margin / 3

                   && intemperies[j].GetComponent<Intemperie>().GetAppearingDistance() > currentDistance - margin / 3)

                {

                    player.GetComponent<PlayerMovement>().SetBaseSpeed(intemperies[j].GetComponent<Intemperie>().GetBaseSpeed());

                    intemperies[j].GetComponent<Intemperie>().TouchPlayer(player.GetComponent<PlayerMovement>());

                    Debug.Log("Im touching the player");

                }

            }

            else

            {

                player.GetComponent<PlayerMovement>().SetBaseSpeed(25);

            }



        }

        return i;

    }





    public void Boost(WeatherType _type)

    {

        boosted[(int)_type] = true;

    }





}