using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer : IFaction
{
    public void DoDamage(IDamagePicker target);
}
