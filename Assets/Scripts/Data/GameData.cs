using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable] // Á÷·ÄÈ­

public class Data
{
    public struct SaveStruct
    {
        public int a;
    }
    public SaveStruct saveStruct;
    public Data(int a)
    {
        saveStruct.a = a;
    }
}