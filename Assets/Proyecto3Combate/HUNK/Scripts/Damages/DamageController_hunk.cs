using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DamageController_hunk : MonoBehaviour, IDamageReceiver_hunk
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ReceiveDamage(IDamageSender_hunk perpetrator, DamagePayload_hunk payload)
    {
        Debug.Log("RECIVIDO");
        bool isAlive = GetComponent<PlayerState>().UpdateHealth(payload.damage);
        Vector3 damageDirection = transform.InverseTransformDirection(payload.position).normalized;

        if (isAlive)
        {
            if (Mathf.Abs(damageDirection.x) >= Mathf.Abs(damageDirection.z))
            {
                anim.SetFloat("DamageX", damageDirection.x * (float) payload.severity);
                anim.SetFloat("DamageY", 0);
            }
            else
            {
                anim.SetFloat("DamageX", 0);
                anim.SetFloat("DamageY", damageDirection.z * (float)payload.severity);
            }
            anim.SetTrigger("Damaged");
        }
        else
        {
            anim.SetTrigger("Die");
        }




    }

    public int Faction => 0;
}
