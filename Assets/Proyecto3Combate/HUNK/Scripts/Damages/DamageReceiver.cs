using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceiver_hunk : IFactionMember_hunk
{
    void ReceiveDamage(IDamageSender_hunk perpetrator, DamagePayload_hunk payload);
}
