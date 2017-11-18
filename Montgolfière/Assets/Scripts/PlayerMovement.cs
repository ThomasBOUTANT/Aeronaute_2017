using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

[System.Serializable]
public enum Damageables { Sail,Flame,Nacelle}



public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float angle,fioulInit,fioulConso;
    [SerializeField]
    private Incrementable speedIncr,burnerIncr,hotAirIncr;
    private float speed,speedMin = 0.1f,minBurn=0.01f;
    private bool isMoving,isBurning,isOff;
    [SerializeField]
    private float baseSpeed,baseBurner,fioul,hotAir;

    [SerializeField]
    private Damageable burner;


    [SerializeField]
    private Damageable[] components;


    void Start () {
        isMoving = false;
        isBurning = false;
        baseSpeed = 25;
        baseBurner = 1;
        fioul = fioulInit;
	}
	
	// Update is called once per frame
	void Update () {

        //////// Update de la position en X avec une base de *baseSpeed* avec inertie
        float newX = transform.position.x + ((speed + baseSpeed) * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180 + speed * angle, 0.0f));
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
        float newZ = transform.position.z + ((-baseBurner+hotAir) * Time.deltaTime);
        if (!isBurning && Mathf.Abs(burner.GetHealth()) > burnerIncr.GetDecr())
            { 

            burner.DamagesTo(Mathf.Sign(burner.GetHealth())*burnerIncr.GetDecr());
        }
        else if (isBurning)
        {

        }
        else
        {
            burner.DamagesTo(burner.GetHealth());
        }
        transform.position = new Vector3(newX, transform.position.y,newZ);

        if ((Mathf.Abs(hotAir) > hotAirIncr.GetDecr()) && !isBurning)
        {
            hotAir -= Mathf.Sign(hotAir) * (hotAirIncr.GetDecr());
        }
        else if(!isBurning)
        {
            hotAir = 0;
        }

    }

    //Move horizontal incrémental pour gérer l'inertie
    public void Move(float _movement)
    {
        if (Mathf.Abs(_movement) > speedMin && !isBurning)
        {
            speed += _movement * speedIncr.GetIncr();
            speed = Mathf.Sign(speed) * Mathf.Min(Mathf.Abs(speed), speedIncr.GetMax());
            isMoving = true;
        }
        else if (isBurning)
        {

        }else
        {
            isMoving = false;
        }
    }
    //Move vertical, gestion du bruleur incrémentale pour gérer l'inertie
    public void Burn(float _burner)
    {
        if (fioul > 0 && _burner>0)
        {

            if (Mathf.Abs(_burner) > minBurn)
            {
                burner.HealTo(_burner * burnerIncr.GetIncr());
                hotAir += hotAirIncr.GetIncr();
                isBurning = true;
                fioul -= fioulConso;
            }
            else
            {
                isBurning = false;
            }
        }else if (_burner < 0)
        {
            hotAir -= hotAirIncr.GetDecr();
            isBurning = true;
        }
        else
        {
            isBurning = false;
        }
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

    public void Damages(Damageables type ,float ammount)
    {
        components[(int)type].DamagesTo(ammount);
    }
}
