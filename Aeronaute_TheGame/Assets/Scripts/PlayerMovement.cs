using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
public enum Damageables { Sail,Flame,Nacelle,Lest}



public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float angle,fioulInit, fioulConso,screenFadingTime, timer;
    [SerializeField]
    private Incrementable speedIncr,burnerIncr,hotAirIncr;
    private float speed,speedMin = 0.1f,minBurn=0.01f;
    private bool isMoving,isBurning, isWatchingEntity;
    [SerializeField]
    private float baseSpeed, baseBurner,scaleAlt,baseCoefLat;
    private float fioul,hotAir;
    [SerializeField]
    private GameObject blackScreen;
    [SerializeField]
    private Color blackscreenColor;
    [SerializeField]
    private float minPlayerZ, maxPlayerZ;

    [SerializeField]
    private Damageable[] components;
    private Damageable burner;
    private Damageable lest;

    private int result;

    [SerializeField]
    private string[] scenes;
    void Start () {
        burner = components[(int)Damageables.Flame];
        lest = components[(int)Damageables.Lest];
        isMoving = false;
        isBurning = false;
        isWatchingEntity = false;
        fioul = fioulInit;
	}
	
	// Update is called once per frame
	void Update () {
        if (isWatchingEntity)
        {
            //Immobilisation du joueur

        }
        else
        {
            //////// Update de la position en X avec une base de *baseSpeed* avec inertie
            float coefLest = (lest.GetHealth() + 2 + baseCoefLat) / 6;
            float coefAlt = (transform.position.z + scaleAlt) / scaleAlt;
            float newX = transform.position.x + ((speed * coefLest + baseSpeed * coefAlt) * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180 + speed * angle * coefLest, 0.0f));
            if (!isMoving && Mathf.Abs(speed) > speedIncr.GetDecr())
            {
                speed -= Mathf.Sign(speed) * (speedIncr.GetDecr());
            }
            else if (isMoving)
            {
            }
            else
            {
                speed = 0;
            }

            //////// Update de la hauteur de la montgolfière avec le brûleur plus gravité de base 
            float newZ = transform.position.z + ((-baseBurner + hotAir) * Time.deltaTime);
            if (!isBurning && Mathf.Abs(burner.GetHealth()) > burnerIncr.GetDecr())
            {

                burner.DamagesTo(Mathf.Sign(burner.GetHealth()) * burnerIncr.GetDecr());
            }
            else if (isBurning)
            {

            }
            else
            {
                burner.DamagesTo(burner.GetHealth());
            }

            if(minPlayerZ > newZ || newZ > maxPlayerZ)
            {
                hotAir = 0;
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(newX, transform.position.y, newZ);
            }


            if ((Mathf.Abs(hotAir) > hotAirIncr.GetDecr()) && !isBurning)
            {
                hotAir -= Mathf.Sign(hotAir) * (hotAirIncr.GetDecr());
            }
            else if (!isBurning)
            {
                hotAir = 0;
            }

            if (fioul == 0)
            {

                if (timer < screenFadingTime)
                {
                    timer += Time.deltaTime;
                    float blend = Mathf.Clamp01(timer / screenFadingTime);
                    blackscreenColor.a = Mathf.Lerp(0.0f, 1.0f, blend);

                    // Apply the resulting color to the material.
                    blackScreen.GetComponent<MeshRenderer>().material.SetColor("_Color", blackscreenColor);
                }
                else
                {
                    //Debug.Log(result);
                    SceneManager.LoadScene(scenes[result + 2]);
                }
            }
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
        if (fioul > 0 && _burner>0 && !isWatchingEntity)
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
        Debug.Log("Im getting damaged by :" + type);
        components[(int)type].DamagesTo(ammount);
    }

    public void LacherLest()
    {
        if (lest.GetHealth() > 0 && baseBurner>0)
        {
            lest.DamagesTo(1);
            baseBurner -= 1;
        }
    }


    public void Damaged(Damageables type)
    {
        switch (type)
        {
            case Damageables.Sail:
                baseBurner++;
                break;
            case Damageables.Nacelle:
                baseCoefLat--;
                break;

            case Damageables.Flame:
                break;
        }


    }


    public void SetEntityWatching(bool _isWatchingEntity)
    {
        isWatchingEntity = _isWatchingEntity;
    }


    public float GetMinPlayerZ()
    {
        return minPlayerZ;
    }

    public float GetMaxPlayerZ()
    {
        return maxPlayerZ;
    }

    public float GetHeight()
    {
        return transform.position.z;
    }



    public void Inclination(int _i)
    {
        result += _i;
    }
}
