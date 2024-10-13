using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class CombactController : MonoBehaviour
{
    [SerializeField] int WeaponMaxIndex;
    /* [SerializeField] GameObject swordMesh;*/
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", true);
        anim.SetInteger("Weapon", 1);
        anim.SetBool("canChange", true);
        anim.SetBool("isIdle", true);
    }

    public void ChangeWeapon(CallbackContext ctx){
        bool val = ctx.performed;
        if (val){
            anim.SetInteger("Weapon", anim.GetInteger("Weapon")+1);
            if(anim.GetInteger("Weapon")> WeaponMaxIndex) anim.SetInteger("Weapon", 1);
            anim.SetTrigger("ChangeWeapon");
            /*InstantiateWeapon(anim.GetInteger("Weapon"));*/
        }
    }

    public void Attack(CallbackContext ctx){
        if(!anim.GetBool("canAttack")) return;
        bool val = ctx.performed;
        if(val){
            anim.SetTrigger("Attack");
            anim.SetBool("canAttack", false);
        } 
    }

    public void OnAttackEnding(){
        anim.SetBool("canAttack", true);
    }

    public void OnWeaponChanged(int value){
        bool Result;
        if(value == 0) Result = false;
        else Result = true;
        anim.SetBool("canChange", Result);
    }

    /*
    public void InstantiateWeapon(int Weapon){
        switch(Weapon){
            default: break;
            case 1:
                swordMesh.SetActive(false);
                break;
            case 2:
                swordMesh.SetActive(true);
                _VFXUpdater.ToggleSword(1,0, swordMesh);
                break;
        }
    }*/
}
