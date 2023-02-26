using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus_Transitions : MonoBehaviour
{

    public void Menu_ActiveAnimation(GameObject NextMenu)
    {
        NextMenu.SetActive(true);
        NextMenu.GetComponent<Animator>().SetBool("Active" , true);
    }
    public void Menu_InactiveAnimation(GameObject CurrentButton)
    {
        CurrentButton.transform.parent.GetComponent<Animator>().SetBool("Active", false);
    }


    public void Menu_SetActive()
    {
        this.gameObject.SetActive(true);
    }
    public void Menu_SetInactive()
    {
        this.gameObject.SetActive(false);
    }
    
}
