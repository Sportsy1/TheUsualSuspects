using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public struct FloatDampener
{
    public float CurrentValue{ get; private set; }
    public float TargetValue {get; set;}
    [field:SerializeField] public float SmoothingTime{get; private set;}
    float velocity;

    public void Update()
    {
        CurrentValue = Mathf.SmoothDamp(CurrentValue, TargetValue, ref velocity, SmoothingTime);
    }
}
