using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry : MonoBehaviour
{
    public static float GetDistance(Transform Object1 , Transform Object2)
    {
        return Mathf.Sqrt( ( (Object1.position.x - Object2.position.x) * ( Object1.position.x - Object2.position.x ) ) + ( (Object1.position.y - Object2.position.y) * (Object1.position.y - Object2.position.y) )
                             + ( (Object1.position.z - Object2.position.z) * (Object1.position.z - Object2.position.z) )  );
    }
}
