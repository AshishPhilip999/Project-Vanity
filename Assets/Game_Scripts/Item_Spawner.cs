using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item_Spawner : MonoBehaviour
{
    public float startx;
    public float endx;
    public float startz;
    public float endz;

    public GameObject[] Drawers;

    public GameObject Key;

     List<Drawer> DrawerComponent;

    // Start is called before the first frame update
    void Start()
    {
        DrawerComponent = new List<Drawer>();
        Drawers = GameObject.FindGameObjectsWithTag("Movable");

        Movable_Object_System.SortByName(Drawers);

        SpawnKeys(Key , 2 , 6);


    }

    public void SpawnKeys(GameObject obj , int min , int max)
    {
       GenerateSpawnPointsDrawer(0.1f);

        int total = Random.Range(min , max);

        for (int i = 0; i < total; i++)
        {
            int randomdrawer = Random.Range(0, DrawerComponent.Count);
            Drawer curr = DrawerComponent[randomdrawer];


            int randompoint = Random.Range(0, curr.points.Count);

            GameObject thisobj = Instantiate(Key, new Vector3(), Quaternion.Euler(90, 0, 0));
            thisobj.transform.SetParent(curr.drawer.transform);

            thisobj.transform.localPosition = curr.points[randompoint];

            thisobj.transform.localScale =   new Vector3(3, 2.014239f, 3);

            curr.points.RemoveAt(randompoint);

            if (curr.points.Count == 0)
            {
                DrawerComponent.RemoveAt(randomdrawer);
            }

        }



    }

    public void GenerateSpawnPointsDrawer(float min_distance)
    {

        foreach(GameObject Drawer in Drawers)
        {
            Drawer currdrawer = new Drawer(Drawer);
            List<Vector3> SpawnPositions = new List<Vector3>();
            BoxCollider collider = Drawer.GetComponent<BoxCollider>();

            startx = collider.center.x - collider.size.x / 2.40f;
            endx = collider.center.x + collider.size.x / 2.40f;

            startz = collider.center.y - collider.size.y / 2.10f;
            endz = collider.center.y + collider.size.y / 2.10f;


            for (float i = startx; i < endx; i += min_distance)
            {
                for (float j = startz; j < endz; j += min_distance)
                {
                    SpawnPositions.Add(new Vector3(i, j, (collider.center.z + (collider.size.z / 2.3f))));
                }
            }
            currdrawer.points = SpawnPositions;

            DrawerComponent.Add(currdrawer);
        }
        
    }
}

class Drawer
{
   public GameObject drawer;

   public List<Vector3> points;

    public Drawer(GameObject drawer)
    {
        this.drawer = drawer;
    }
}
