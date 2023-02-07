using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ViewThis : MonoBehaviourPunCallbacks
{
    public GameObject Lamp;

    public GameObject[] Lights;

    public string key;

    private void Start()
    {
        Lights = GameObject.FindGameObjectsWithTag("Light Source");
    }

    // Start is called before the first frame update
    void Update()
    {
        if(key == "On")
        {
            foreach(GameObject obj in Lights)
            {
                obj.SetActive(true);
            }
        }else if(key == "Off")
        {
            foreach (GameObject obj in Lights)
            {
                obj.SetActive(false);
            }
        }
    }
}
