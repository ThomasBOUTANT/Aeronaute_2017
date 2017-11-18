using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] entities;
    private int nb_entities;
    private float timer = 0;

    private float currentDistance;

    GameObject player;

    [SerializeField]
    private float margin;

    // Use this for initialization
    void Start () {

        nb_entities = entities.Length;
        player = GameObject.FindGameObjectWithTag("Player");
        currentDistance = 0;

        for (int j = 0; j < nb_entities; j++)
        {
            entities[j].SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        currentDistance = player.GetComponent<PlayerMovement>().GetCurrentDistance();

        CheckEntities();



    }

    void CheckEntities()
    {
        int i = -1;
        for (int j = 0; j < nb_entities; j++)
        {

            if (!entities[j].activeSelf
                && entities[j].GetComponent<Entity>().GetAppearingDistance() < currentDistance + margin
                && entities[j].GetComponent<Entity>().GetAppearingDistance() > currentDistance - margin
                )
            {
                //On active l'entity à l'endroit du joueur
                entities[j].SetActive(true);
                timer = Time.time;

                //Blocage du joueur
                player.GetComponent<PlayerMovement>().SetEntityWatching(true);

               // i = j;
                break;
            }

            else if (entities[j].activeSelf)
            {
                if (Time.time > timer + entities[j].GetComponent<Entity>().GetStayingTime())
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //Pacte avec l'entité
                        entities[j].GetComponent<Entity>().SetIsFriend(-1);
                        player.GetComponent<PlayerMovement>().Inclination(-1);
                        player.GetComponent<PlayerMovement>().SetEntityWatching(false);
                        entities[j].GetComponent<Entity>().DisableText();
                    }
                    else if (Input.GetAxis("Horizontal") != 0)
                    {
                        //Pas de pacte avec l'entité
                        entities[j].GetComponent<Entity>().SetIsFriend(1);
                        player.GetComponent<PlayerMovement>().Inclination(1);
                        player.GetComponent<PlayerMovement>().SetEntityWatching(false);
                        entities[j].GetComponent<Entity>().DisableText();
                    }
                }
                else if (entities[j].GetComponent<Entity>().IsFriend() != 0)
                {
                    entities[j].GetComponent<Entity>().Disappear();
                }
            }
            
        }


    }
}
