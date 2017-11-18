using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Damageable {

    [SerializeField]
    float healthPoints;

    [SerializeField]
    float maxHealtPoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
