using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageSender_hunk : IFactionMember
{
    void SendDamage(IDamageReceiver_hunk target);
}
