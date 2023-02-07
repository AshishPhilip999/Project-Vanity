using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCulling : MonoBehaviour
{
    Transform Player;
    
    public List<GameObject> children;

    public GameObject Culler;

    void Start()
    {
         Culler.SetActive(false);
         GetChildren(gameObject);
    }

    void GetChildren(GameObject Parent)
    {

        if (Parent.transform.childCount == 0)
        {

            if(Parent.GetComponent<MeshRenderer>() != null)
            {
                Parent.GetComponent<MeshRenderer>().enabled = false;
                children.Add(Parent);
            }
            return;
        }else
        {
            for(int i = 0; i < Parent.transform.childCount; i++)
            {
                GetChildren( Parent.transform.GetChild(i).gameObject );
            }
        }
    }

    void DisableChildrenMesh()
    {
        foreach(GameObject child in children )
        {
            child.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void EnableChildrenMesh()
    {
        foreach (GameObject child in children)
        {
            child.GetComponent<MeshRenderer>().enabled = true;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Culler")
        {
            Culler.SetActive(true);
            EnableChildrenMesh();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Culler")
        {
            Culler.SetActive(false);
            DisableChildrenMesh();
        }

    }
}
