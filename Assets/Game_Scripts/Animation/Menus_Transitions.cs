using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus_Transitions : MonoBehaviour
{

    public void Menu_ActiveAnimation(GameObject NextMenu)
    {
        SetButtons(NextMenu, true);
        NextMenu.SetActive(true);
        NextMenu.GetComponent<Animator>().SetBool("Active" , true);
    }
    public void Menu_InactiveAnimation(GameObject CurrentMenu)
    {
        SetButtons(CurrentMenu , false);
        CurrentMenu.GetComponent<Animator>().SetBool("Active", false);
    }


    public void Menu_SetActive()
    {
        this.gameObject.SetActive(true);
    }
    public void Menu_SetInactive()
    {
        this.gameObject.SetActive(false);
    }


    void SetButtons(GameObject Menu , bool State)
    {
        for(int i = 0; i < Menu.transform.childCount; i++)
        {
            GameObject CurrentButton = Menu.transform.GetChild(i).gameObject;
            if ( CurrentButton.GetComponent<Button>() != null  )
            {
                CurrentButton.GetComponent<Image>().enabled = State;
                CurrentButton.GetComponent<Button>().enabled = State;
            }
        }
    }

}
