using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Animation : MonoBehaviour
{
    AnimationCurve anim;
    Keyframe[] ks;

    void Start()
    {
        ks = new Keyframe[50];
        for (var i = 0; i < ks.Length; i++)
        {
            ks[i] = new Keyframe(i, i );
        }
        anim = new AnimationCurve(ks);
    }


    void Update()
    {
        transform.position = new Vector3(Time.time, anim.Evaluate(Time.time), 0) * 5;
    }
}
