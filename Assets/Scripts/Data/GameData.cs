using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable] // ����ȭ

public class SaveData
{
    public struct SaveStruct
    {
        public int a;
    }
    public SaveStruct saveStruct;
    public SaveData(int a)
    {
        saveStruct.a = a;
    }
}