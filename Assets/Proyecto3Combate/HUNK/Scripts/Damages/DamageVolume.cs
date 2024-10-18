using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume_hunk : MonoBehaviour, IDamageSender_hunk
{
    [SerializeField]
    private float damageAmount;
    [SerializeField]
    private DamagePayload_hunk.DamageSeverity severity;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("adios");
        if (other.TryGetComponent(out IDamageReceiver_hunk receiver))
        {
            Debug.Log("ola");
            SendDamage(receiver);
        }
    }

    public void SendDamage(IDamageReceiver_hunk target)
    {
        Debug.Log("mandado");
        target.ReceiveDamage(this, new DamagePayload_hunk { damage = -damageAmount, severity = severity, position = transform.position });
    }

    public int Faction => 1;
}
