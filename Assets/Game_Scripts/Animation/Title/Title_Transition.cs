using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Transition : MonoBehaviour
{
    public GameObject transition;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            transition.SetActive(true);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Loading Screen");
    }
}
