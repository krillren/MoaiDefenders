using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MaterialsType
{
    None,
    Wood, //RED
    Glass //GREEN
}

public static class TypeExtensions
{
    public static Color ToColor(this MaterialsType type)
    {
        return type switch
        {
            MaterialsType.Wood => Color.red,
            MaterialsType.Glass => Color.green,
            _ => Color.grey
        };
    }
}
