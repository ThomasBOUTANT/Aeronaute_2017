using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public class Damageable : MonoBehaviour {


    [SerializeField]
    private Damageables type;
    [SerializeField]
    private float healthPoints, maxHealtPoints,minHealthPoints;

    [SerializeField]
    private float[] stages;

    [SerializeField]
    private Material[] stagesSprites;

    [SerializeField]
    private PlayerMovement player;

    //Etat de la montgolfière : 0 = intact, 1 = damaged, 2 = broken
    int state;

	// Use this for initialization
	void Start () {
        state = 0;
	}
	/*
	// Update is called once per frame/*
	void Update () {
        
        //Changements d'états qui conduiront à un changment de sprite
        if((state<stages.Length) && (healthPoints < stages[state]))
        {
            Debug.Log(stages[0]);
            ChoseSprite(state);
            state++;
            player.Damaged(type);

        }
	}
    */
    void Update()
    {


        if (stages.Length != 0)
        {
            //Changements d'états qui conduiront à un changement de sprite
            if ((state < stages.Length) && (healthPoints < stages[state]))
            {
                //Debug.Log(stages[0]);
                ChoseSprite(state);
                state++;
                player.Damaged(type);
            }
            else if ((state > 0) && (state < stages.Length) && (healthPoints > stages[state-1]))
            {
                state--;
                ChoseSprite(state);

            }
        }
    }

    public void HealTo(float heal)
    {
        if (healthPoints + heal < maxHealtPoints)
        {
            healthPoints = healthPoints + heal;
        }
        else
        {
            healthPoints = maxHealtPoints;
        }
    }

    public void DamagesTo(float damages)
    {
        healthPoints = Mathf.Max(healthPoints - damages,minHealthPoints);
    }

    public void ChoseSprite(int _state)
    {
        GetComponent<Renderer>().material = stagesSprites[_state];
    }

    public float GetHealth()
    {
        return healthPoints;
    }
}
