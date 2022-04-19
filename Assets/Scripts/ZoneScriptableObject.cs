using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ZoneScriptableObject", order = 1)]
public class ZoneScriptableObject : ScriptableObject
{

    public ZoneID ID;

    public int ZoneOpenCost;

}
