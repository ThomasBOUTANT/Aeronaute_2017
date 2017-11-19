using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour {

    [SerializeField]
    private WeatherType type;

    [SerializeField]
    private float appearingDistance, stayingTime, startingTime,fadingTime, time;
    //private Intemperie appearingIntemperie;

    //Object donné si l'on pactise avec l'entité
    [SerializeField]
    private GameObject givenComponent, entityTextA,entityTextB, childFG, childBG;

    [SerializeField]
    private string[] entityProp1,entityProp2;

    [SerializeField]
    private WeatherManager wheaterManager;

    [SerializeField]
    private Intemperie intemperie;

    [SerializeField]
    private Light entityLight;

    public MeshRenderer meshRendererFG, meshRendererBG;
    private Color entityColorFG, entityColorBG;
    private float entityLightIntensity;
    //Position du joueur par rapport à l'entité : 0 - neutre / 1 - Sans pacte / -1 - Avec Pacte
    [SerializeField]
    private int isFriend;

    private bool disabledText;

    // Use this for initialization
    void Awake () {
        //entityTextA.SetActive(false);
        meshRendererFG = childFG.GetComponent<MeshRenderer>();
        meshRendererBG = childBG.GetComponent<MeshRenderer>();
        isFriend = 0;
        entityColorFG = childFG.GetComponent<MeshRenderer>().material.color;
        entityColorBG = childBG.GetComponent<MeshRenderer>().material.color;
        time = 0;
        entityLightIntensity = entityLight.intensity;
        disabledText = false;

    }

    //Fondu en apparition


// Update is called once per frame
void Update () {
        if (meshRendererFG == null)
        {
            meshRendererFG = childFG.GetComponent<MeshRenderer>();
        }
        if (meshRendererBG == null)
        {
            meshRendererBG = childBG.GetComponent<MeshRenderer>();
        }

        if (time < fadingTime && entityColorFG.a < 1.0f)
        {
            time += Time.deltaTime;
            float blend = Mathf.Clamp01(time / fadingTime);
            entityColorFG.a = Mathf.Lerp(0.0f, 1.0f, blend);
            entityColorBG.a = Mathf.Lerp(0.0f, 1.0f, blend);
            entityLightIntensity = entityLightIntensity + 0.001f;

            // Apply the resulting color to the material.
            meshRendererFG.material.SetColor("_Color", entityColorFG);
            meshRendererBG.material.SetColor("_Color", entityColorBG);
            entityLight.intensity = entityLightIntensity;

        }
        if (gameObject.activeSelf && !disabledText)
        {
            string propA = "A:" ;
            string propB="B:";
            int i = 0;

            entityTextA.SetActive(true);
            for (i = 0; i < entityProp1.Length; i++)
            {
                propA = propA + entityProp1[i] + "\n";
            }
            entityTextA.GetComponent<Text>().text = propA;


            entityTextB.SetActive(true);
            for (i = 0; i < entityProp2.Length; i++)
            {
                propB = propB + entityProp2[i] + "\n";
            }
            entityTextB.GetComponent<Text>().text = propB;
        }
        else
        {

            //entityText.SetActive(false);
            entityTextA.GetComponent<Text>().text = " ";
            entityTextB.GetComponent<Text>().text = " ";
        }
		
	}

    public float GetAppearingDistance()
    {
        entityTextA.SetActive(true);
        entityTextB.SetActive(true);
        return appearingDistance;
    }

    public void SetIsFriend(int _isFriend)
    {
        isFriend = _isFriend;
        if (_isFriend == -1)
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
        disabledText = true;
        entityTextA.GetComponent<Text>().text = " ";
        entityTextB.GetComponent<Text>().text = " ";
        //entityText.SetActive(false);
    }


    public int IsFriend()
    {
        return isFriend;
    }


    public void Disappear()
    {
        time = 0;
        if (time < fadingTime)
        {
            time += Time.deltaTime;
            float blend = Mathf.Clamp01(time / fadingTime);
            entityColorFG.a = Mathf.Lerp(1.0f, 0.0f, blend);
            entityColorBG.a = Mathf.Lerp(1.0f, 0.0f, blend);
            entityLightIntensity = entityLightIntensity - 0.001f;

            // Apply the resulting color to the material.
            meshRendererFG.material.SetColor("_Color", entityColorFG);
            meshRendererBG.material.SetColor("_Color", entityColorBG);
            entityLight.intensity = entityLightIntensity;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
