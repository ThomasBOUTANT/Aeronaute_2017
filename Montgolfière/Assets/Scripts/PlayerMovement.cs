using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Damageables { Sail, Flame, Nacelle };


//Class générique pour gérer les valeur incrémentales par palier fixe
[System.Serializable]
public class Incrementable
{
    [SerializeField]
    private float incr, decr, max;
    public float GetIncr()
    {
        return incr;
    }

    public float GetDecr()
    {
        return decr;
    }

    public float GetMax()
    {
        return max;
    }
}


public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private Incrementable speedIncr,burnerIncr;
    private float speed, burner,speedMin = 0.1f,minBurn=0.01f;
    private bool isMoving,isBurning, isOff;
    private float baseSpeed,baseBurner;

    [SerializeField]
    private Damageable[] components; //rouge = normal

    void Start () {
        isMoving = false;
        isBurning = false;
        baseSpeed = 10;
        baseBurner = 1;
	}
	
	// Update is called once per frame
	void Update () {

        //////// Update de la position en X avec une base de *baseSpeed* avec inertie
        float newX = transform.position.x + ((speed + baseSpeed) * Time.deltaTime);
        if (!isMoving && Mathf.Abs(speed) > speedIncr.GetDecr())
        {
            speed -= Mathf.Sign(speed) * (speedIncr.GetDecr());
        }
        else if(isMoving)
        {
        }
        else
        {
            speed = 0;
        }

        //////// Update de la hauteur de la montgolfière avec le brûleur plus gravité de base 
        float newY = transform.position.y + ((burner-baseBurner) * Time.deltaTime);
        if (!isBurning && Mathf.Abs(burner) > burnerIncr.GetDecr())
            { 

            burner -= Mathf.Sign(burner)*burnerIncr.GetDecr();
        }
        else if (isBurning)
        {

        }
        else
        {
            burner = 0;
        }
        transform.position = new Vector3(newX, newY);


    }

    //Move horizontal incrémental pour gérer l'inertie
    public void Move(float _movement)
    {
        if (Mathf.Abs(_movement) > speedMin)
        {
            speed += _movement * speedIncr.GetIncr();
            speed = Mathf.Sign(speed) * Mathf.Min(Mathf.Abs(speed), speedIncr.GetMax());
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    //Move vertical, gestion du bruleur incrémentale pour gérer l'inertie
    public void Burn(float _burner)
    {
        if (Mathf.Abs(_burner) > minBurn)
        {
            burner += _burner * burnerIncr.GetIncr();
            burner = Mathf.Sign(burner) * Mathf.Min(Mathf.Abs(burner), burnerIncr.GetMax());
            isBurning = true;
        }
        else
        {
            isBurning = false;
        }
    }

    public void TakeDamages(Damageables type, float damages)
    {
        components[(int)type].DamagesTo(damages);
    }

    // Setter sur baseSpeed et baseBurner pour gérer l'inertie
    public void SetBaseSpeed(float _baseSpeed)
    {
        baseSpeed = _baseSpeed;
    }

    public void SetBaseBurner(float _baseBurner)
    {
        baseBurner = _baseBurner;
    }


    public float GetCurrentDistance()
    {
        return transform.position.x;
    }


}
