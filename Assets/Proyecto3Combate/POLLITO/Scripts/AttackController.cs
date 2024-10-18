using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext; 

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterVFXUpdater))]
public class AttackController : MonoBehaviour
{
    [Tooltip("Starts in 1, so if you have 3 weapons available, the int will be 3, and so on")] 
    [SerializeField] int WeaponMaxIndex;
    [SerializeField] GameObject swordMesh;
    private Animator anim;
    private CharacterVFXUpdater _VFXUpdater;

    void Awake()
    {
        _VFXUpdater = GetComponent<CharacterVFXUpdater>();
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", true);
        anim.SetInteger("Weapon", 1);
        anim.SetBool("canChange", true);
    }

    public void ChangeWeapon(CallbackContext ctx){
        bool val = ctx.performed;
        if (val){
            anim.SetInteger("Weapon", anim.GetInteger("Weapon")+1);
            if(anim.GetInteger("Weapon")> WeaponMaxIndex) anim.SetInteger("Weapon", 1);
            anim.SetTrigger("ChangeWeapon");
            InstantiateWeapon(anim.GetInteger("Weapon"));
        }
    }

    public void HeavyAttack(CallbackContext ctx){
        if(!anim.GetBool("canAttack")) return;
        bool val = ctx.performed;
        if(val){
            anim.SetBool("HeavyAttack", true);
            anim.SetTrigger("Attack");
            anim.SetBool("canAttack", false);
            
        } 
    }

    public void LightAttack(CallbackContext ctx){
        if(!anim.GetBool("canAttack")) return;
        bool val = ctx.performed;
        if(val){
            anim.SetBool("HeavyAttack", false);
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
    }
}
