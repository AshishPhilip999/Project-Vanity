using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable_Object_System : MonoBehaviour
{
    public GameObject[] Movables;
    public GameObject[] Doors;

    private void Start()
    {
        Movables = GameObject.FindGameObjectsWithTag("Movable");
        SortByName(Movables);
        Doors = GameObject.FindGameObjectsWithTag("Door");
        SortByName(Doors);
    }

   public static void SortByName(GameObject[] Objects)
    {
        for(int i = 0; i < Objects.Length - 1; i++)
        {
            for(int j = i+1; j < Objects.Length; j++)
            {
                if( SingleParse(Objects[i].name) > SingleParse(Objects[j].name)  )
                {
                    GameObject temp = Objects[i];
                    Objects[i] = Objects[j];
                    Objects[j] = temp;
                }
            }
        }
    }

   public static int SingleParse(string ObjectName)
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
