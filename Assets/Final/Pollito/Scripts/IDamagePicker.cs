using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagePicker : IFaction
{
    public void RecieveDamage(IDamageDealer dealer, SDamageInfo damageInfo);
}
