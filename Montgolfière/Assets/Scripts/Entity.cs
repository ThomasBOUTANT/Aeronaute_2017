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
    private GameObject givenComponent, entityText, childFG, childBG;


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

    // Use this for initialization
    void Start () {
        entityText.SetActive(false);
        meshRendererFG = childFG.GetComponent<MeshRenderer>();
        meshRendererBG = childBG.GetComponent<MeshRenderer>();
        isFriend = 0;
        entityColorFG = childFG.GetComponent<MeshRenderer>().material.color;
        entityColorBG = childBG.GetComponent<MeshRenderer>().material.color;
        time = 0;
        entityLightIntensity = entityLight.intensity;

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
        Debug.Log("j'essaie de desactiver le text");
        entityText.SetActive(false);
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
