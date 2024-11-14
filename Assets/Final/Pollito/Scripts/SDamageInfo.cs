using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
public struct SDamageInfo
{
    public enum DamageRecoil
    {
        light = 0,
        mid = 1,
        heavy = 2
    }

    public int damage;
    public Vector2 direction;
}
