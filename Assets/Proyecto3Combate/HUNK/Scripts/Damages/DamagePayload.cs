using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamagePayload_hunk 
{
    public enum DamageSeverity
    {
        light = 1,
        strong = 2,
        crit = 3
    }


    public float damage;
    public Vector3 position;
    public DamageSeverity severity;
}
