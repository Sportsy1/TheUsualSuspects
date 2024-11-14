using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMaster : MonoBehaviour, IDamageDealer, IDamagePicker, IFaction
{
    private Animator animator;
    [SerializeField] private int faction;
    [SerializeField] private bool isEnvironmental;
    private Chick_CharacterState characterState;

    void Start()
    {
        animator = GetComponent<Animator>();
        if(!isEnvironmental)
            characterState = GetComponent<Chick_CharacterState>();
    }

    public void DoDamage(IDamagePicker target){

    }

    public void RecieveDamage(IDamageDealer dealer, SDamageInfo damageInfo){
        if(isEnvironmental) return;
        characterState.ChangeHealth(damageInfo.damage);
        /*if(characterState.BreakResistance <= (1f * (float) damageInfo.DamageRecoil)){

        }*/
    }

    public int Faction => faction;
}
