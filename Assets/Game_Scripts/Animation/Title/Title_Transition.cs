﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Transition : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Loading Screen");
    }
}
