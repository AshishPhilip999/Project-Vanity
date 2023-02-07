using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsItem : MonoBehaviour
{
    public GameObject RotationParent;


    // Update is called once per frame
    void Update()
    {
        transform.rotation = RotationParent.transform.rotation;
    }
}
