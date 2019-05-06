using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFilter : MonoBehaviour
{
    public static Filter[] filters =
    {
        new Filter("fuck", "frick"),
        new Filter("FUCK", "FRICK"),
        new Filter("Fuck", "Frick"),
        new Filter("FuCk", "FrIcK"),
        new Filter("shit", "shoot"),
        new Filter("SHIT", "SHOOT"),
        new Filter("Shit", "Shoot"),
        new Filter("crap", "crud"),
        new Filter("CRAP", "CRUD"),
        new Filter("Crap", "Crud")
    };
}

public struct Filter
{
    public string target;
    public string goal;
    public Filter(string t, string g)
    {
        target = t;
        goal = g;
    }
}