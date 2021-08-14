using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public enum TEAM
    {
        UNASSIGNED = 0,
        TERRORISTS = 1,
        COUNTERTERRORISTS = 2
    }

    //segundos até plantar a bomba
    public static float TIME_TO_PLANT_BOMB = 20;
}
