using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_System : MonoBehaviour
{
    public GameObject[] LightSource;
    public GameObject[] RoofLightSwitche;
    public GameObject[] RoofLight;

    public bool[] LightSourceStatuts;
    public bool[] RoofLightSwitchStatus;

    public Material BrightLight;
    public Material DimLight;
    public Material RoofDimLight;

    

    private void Start()
    {
        LightSource = GameObject.FindGameObjectsWithTag("Light");
        SortByName(LightSource);

        RoofLightSwitche = GameObject.FindGameObjectsWithTag("Roof Light Switch");
        SortByName(RoofLightSwitche);

        RoofLight = GameObject.FindGameObjectsWithTag("Roof Light");
        SortByName(RoofLight);

        LightSourceStatuts = new bool[LightSource.Length];
        RoofLightSwitchStatus = new bool[RoofLightSwitche.Length];
        
        // Lamp status Check
        for(int i = 0; i < LightSourceStatuts.Length; i++)
        {
           if(LightSource[i].GetComponent<Light>().enabled)
            {
                LightSourceStatuts[i] = true;
            }
            else
            {
                LightSourceStatuts[i] = false;
            }
        }

        // roof status check
        for(int i = 0; i < RoofLightSwitchStatus.Length; i++)
        {
            if (RoofLightSwitche[i].transform.rotation == Quaternion.Euler( new Vector3(-89.98f,0,0) ))
            {
                RoofLightSwitchStatus[i] = false;
            }
            else
            {
                RoofLightSwitchStatus[i] = true;
            }
        }
    }

    void SortByName(GameObject[] Objects)
    {
        for (int i = 0; i < Objects.Length - 1; i++)
        {
            for (int j = i + 1; j < Objects.Length; j++)
            {
                if (SingleParse(Objects[i].name) > SingleParse(Objects[j].name))
                {
                    GameObject temp = Objects[i];
                    Objects[i] = Objects[j];
                    Objects[j] = temp;
                }
            }
        }
    }

    public int SingleParse(string ObjectName)
    {
        int index;
        string stringindex = ObjectName.Substring(1, ObjectName.Length - 1);
        if (int.TryParse(stringindex, out index))
        {
            return index;
        }

        return -1;
    }
}
