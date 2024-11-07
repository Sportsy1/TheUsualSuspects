using System;
using UnityEngine;

[Serializable]
public struct QuaternionDampener
{
    public Quaternion CurrentValue{ get; private set; }
    public Quaternion TargetValue {get; set;}
    [field:SerializeField] public float SmoothingSpeed{get; private set;}

    public void Update()
    {
        CurrentValue = Quaternion.Slerp(CurrentValue, TargetValue, SmoothingSpeed * Time.deltaTime);
    }
}
