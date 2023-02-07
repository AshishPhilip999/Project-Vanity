using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacher : MonoBehaviour
{
    Object[] Meshes;

    //Start is called before the first frame update
    void Start()
    {
        Meshes = Resources.FindObjectsOfTypeAll(typeof(GameObject));

        foreach(GameObject currObj in Meshes)
        {
            if(currObj.tag == "Light Source" || currObj.tag == "Walls")
            {
                continue;
            }

            if(currObj.GetComponent<MeshRenderer>() != null)
            {
                currObj.AddComponent<DistanceCulling>();
                if (currObj.GetComponent<BoxCollider>() != null && currObj.GetComponent<MeshCollider>() != null)
                {
                    continue;
                }
                else
                {
                    currObj.AddComponent<BoxCollider>();
                }
                currObj.AddComponent<Rigidbody>();
                currObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
