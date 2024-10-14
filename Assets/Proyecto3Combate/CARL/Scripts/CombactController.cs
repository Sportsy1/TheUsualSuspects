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
    private Animator anim;
    private int comboIndex = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", true);
        anim.SetInteger("Weapon", 1);
        anim.SetBool("canChange", true);
        anim.SetBool("isIdle", true);
    }

    public void ChangeWeapon(CallbackContext ctx){
        if (!anim.GetBool("canChange")) return;

        bool val = ctx.performed;
        if (val){
            int currentWeapon = anim.GetInteger("Weapon");
            currentWeapon = (currentWeapon % WeaponMaxIndex) + 1; // Alterna entre 1 y 2
            anim.SetInteger("Weapon", currentWeapon);

            anim.SetTrigger("ChangeWeapon");
            Debug.Log("Cambio de arma, nueva arma: " + currentWeapon);
        }
    }

    public void Attack(CallbackContext ctx){
        if (!anim.GetBool("canAttack")) return; // No permitas ataques si no se puede atacar

        bool val = ctx.performed;
        if (val)
        {
            anim.SetTrigger("Attack");

            // Si el ataque se ha realizado correctamente, avanza el combo
            comboIndex++;
            anim.SetInteger("ComboIndex", comboIndex); // Actualiza el Animator

            // Bloquea el ataque hasta que el combo avance
            anim.SetBool("canAttack", false);
            Debug.Log("Ataque realizado, comboIndex: " + comboIndex);
        }
    }

    public void OnAttackEnding(){
        // Al final de cada ataque, permite seguir atacando y cambiar de paradigma
        anim.SetBool("canAttack", true);

        // Resetea el combo si llega al máximo de ataques
        if (comboIndex >= 5) // Máximo de combo para Punch es 5, para Kick sería otro valor
        {
            comboIndex = 0;
        }

        Debug.Log("Ataque terminado, canAttack activado.");
    }

    public void OnWeaponChanged(int value){
        bool Result;
        if(value == 0) Result = false;
        else Result = true;
        anim.SetBool("canChange", Result);
    }
}
