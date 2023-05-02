using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Element_Movable : MonoBehaviour
{
    public bool thisElement;
    public float sensitivity;

    public GameObject UIElement;

    RectTransform UIObj;

    void Start()
    {
        if(thisElement)
        {
            UIElement = gameObject;
        }

        UIObj = UIElement.GetComponent<RectTransform>();

    }



    public void MoveUIELement()
    {
        if (UIObj.localPosition.x < -1070.0f)
        {
            UIObj.localPosition = new Vector3(-1070.0f, UIObj.localPosition.y, 0);
            return;
        }

        if(UIObj.localPosition.x > 176.5f)
        {
            UIObj.localPosition = new Vector3(176.5f, UIObj.localPosition.y, 0);
            return;
        }



        if (UIObj.localPosition.y < -120.0f)
        {
            UIObj.localPosition = new Vector3(UIObj.localPosition.x, -120.0f , 0);
            return;
        }

        if (UIObj.localPosition.y > 306.0f)
        {
            UIObj.localPosition = new Vector3(UIObj.localPosition.x, 306.0f , 0);
            return;
        }

        float MouseX = Input.GetAxis("Mouse X") * (sensitivity );
       float MouseY = Input.GetAxis("Mouse Y") * (sensitivity );


        UIObj.localPosition += new Vector3(MouseX, MouseY, 0);

    }
}
