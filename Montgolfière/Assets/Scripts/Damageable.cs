using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public class Damageable : MonoBehaviour {

    [SerializeField]
    private float healthPoints, maxHealtPoints;

    [SerializeField]
    private float[] stages;

    [SerializeField]
    private Material[] stagesSprites;

    //Etat de la montgolfière : 0 = intact, 1 = damaged, 2 = broken
    int state;

	// Use this for initialization
	void Start () {
        state = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
        //Changements d'états qui conduiront à un changment de sprite
        if(state<stages.Length && healthPoints < stages[state])
        {
            ChoseSprite(state);
            state++;

        }
	}

    public void HealTo(float heal)
    {
        if (healthPoints + heal < maxHealtPoints)
        {
            healthPoints = healthPoints + heal;
        }
    }

    public void DamagesTo(float damages)
    {
        healthPoints = healthPoints - damages;
    }

    public void ChoseSprite(int _state)
    {
        GetComponent<Renderer>().material = stagesSprites[_state];
    }
}
