using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour {

    [SerializeField]
    private WeatherType type;

    [SerializeField]
    private float appearingDistance, stayingTime, startingTime;
    //private Intemperie appearingIntemperie;

    //Object donné si l'on pactise avec l'entité
    [SerializeField]
    private GameObject givenComponent, entityText;


    [SerializeField]
    private WeatherManager wheaterManager;

    [SerializeField]
    private Intemperie intemperie;
    //Position du joueur par rapport à l'entité : 0 - neutre / 1 - Sans pacte / -1 - Avec Pacte
    [SerializeField]
    private int isFriend;

    // Use this for initialization
    void Start () {
        entityText.SetActive(false);
        isFriend = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.activeSelf)
        {
            entityText.SetActive(true);
        }
        else
        {
            entityText.SetActive(false);
        }
		
	}

    public float GetAppearingDistance()
    {
        return appearingDistance;
    }

    public void SetIsFriend(int _isFriend)
    {
        isFriend = _isFriend;
        if (_isFriend == 1)
        {
            intemperie.Disable();
            wheaterManager.Boost(type);
        }
        else
        {
            intemperie.Boost();
        }
    }

    public float GetStayingTime()
    {
        return stayingTime;
    }

    public void DisableText()
    {
        Debug.Log("j'essaie de desactiver le text");
        entityText.SetActive(false);
    }
}
