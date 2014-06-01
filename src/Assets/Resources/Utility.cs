using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public static class EnumUtil 
{
    public static IEnumerable<T> GetValues<T>() 
    {
        return (T[])Enum.GetValues(typeof(T));
    }
    
    public static T FromString<T> (string value)
    {
        return (T)Enum.Parse (typeof(T), value);
    }
}

public static class Utility
{
    public static void Perspectivize(GameObject o)
    {
        int sortingOrder = (int)(4 * (-o.transform.position.y + Camera.main.orthographicSize));
        o.GetComponent<SpriteRenderer> ().sortingOrder = (int)(sortingOrder);
    }
}