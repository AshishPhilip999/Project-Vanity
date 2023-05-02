using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item_Spawner : MonoBehaviour
{
    public GameObject[] SpawnPositions;

    public GameObject keys;

    public GameObject Pen;

    public int TotalKeys;

    List<int> randomslist;

    // Start is called before the first frame update
    void Start()
    {
        randomslist = new List<int>();
        SpawnPositions = GameObject.FindGameObjectsWithTag("SpawnPosition");

        for(int i = 0; i < SpawnPositions.Length; i++)
        {
            randomslist.Add(i);
        }

        int TotalKeys = Random.Range(1,4);
        int TotalPens = Random.Range(3,4);

        

        for(int i = 0; i < TotalKeys; i++)
        {
            int randomIndex = GetRandomPosition();

            int randomY = Random.Range(0, 360);

            GameObject key = PhotonNetwork.Instantiate(keys.name , SpawnPositions[randomIndex].transform.position , Quaternion.identity );

            key.transform.rotation = Quaternion.Euler(90,0,0);
            key.transform.position += new Vector3(0 , 0.009f , 0);

            key.transform.SetParent(SpawnPositions[randomIndex].transform , true);

        }

        //pens
        for (int i = 0; i < TotalPens; i++)
        {
            int randomIndex = GetRandomPosition();

            int randomY = Random.Range(0, 180);

            GameObject pen = PhotonNetwork.Instantiate(Pen.name, SpawnPositions[randomIndex].transform.position, Quaternion.identity);

            pen.transform.rotation = Quaternion.Euler(0, 0, 0);
            pen.transform.position += new Vector3(0, 0.009f, 0);

            pen.transform.SetParent(SpawnPositions[randomIndex].transform, true);

        }


    }

    public int GetRandomPosition()
    {
        int index = Random.Range(0 , randomslist.Count);

        int num = randomslist[index];

        randomslist.RemoveAt(index);

        return num;
    }
}
