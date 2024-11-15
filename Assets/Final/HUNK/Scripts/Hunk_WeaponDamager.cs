using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunk_WeaponDamager : MonoBehaviour, IDamageSender_hunk
{
    [SerializeField]
    private float baseDamage = 10f;
    private float multiplier;
    private List<IDamageReceiver_hunk> hitReceivers = new List<IDamageReceiver_hunk>();

    public int Faction => 0;

    public void SendDamage(IDamageReceiver_hunk target)
    {
        DamagePayload_hunk damagePayload = new DamagePayload_hunk { damage = baseDamage * multiplier, position = transform.position, severity = DamagePayload_hunk.DamageSeverity.light };
        target.ReceiveDamage(this, damagePayload);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("ola2");
        if (collision.TryGetComponent(out IDamageReceiver_hunk target) && target.Faction != Faction)// !hitReceivers.Contains(target))
        {
            hitReceivers.Add(target);
            Debug.Log("ola");
            SendDamage(target);
        }
    }

    public void Toggle(float multiplier)
    {
        hitReceivers.Clear();
        Collider col = GetComponent<Collider>();
        col.enabled = !col.enabled;
        this.multiplier = multiplier;
    }
}
