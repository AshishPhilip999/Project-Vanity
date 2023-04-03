using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Functions : MonoBehaviour
{
    public AudioSource ThemeBackgroundmusic;

    // Start is called before the first frame update
   
    public void Play_Theme()
    {
        ThemeBackgroundmusic.Play();
    }
}
