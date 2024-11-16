using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class DamageController_hunk : MonoBehaviour, IDamageReceiver_hunk
{
    private Animator anim;
    [SerializeField]
    private int faction;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ReceiveDamage(IDamageSender_hunk perpetrator, DamagePayload_hunk payload)
    {
        bool isAlive = GetComponent<PlayerState>().UpdateHealth(-payload.damage);
        //Debug.Log("is alive = " + isAlive);
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
            if(GetComponent<PlayerInput>() != null)
                anim.SetBool("canAttack", true);
        }
        else
        {
            anim.SetTrigger("Die");
            //if(TryGetComponent<PlayerInput>(out PlayerInput myInput)) myInput.enabled = false;
            if(TryGetComponent<MutantAI>(out MutantAI mutantAI)){
                mutantAI.enabled = false;
                GetComponent<Collider>().enabled = false;
                SendMessage("UnBugCollider");
            }
            
        }




    }

    public int Faction => faction;
}
