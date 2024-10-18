using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Animator))]
public class Attack : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private GameObject sword;

    private bool AttackActive()
    {
        return anim.GetFloat("ActiveAttack") > 0.5f;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (AttackActive()) return;
            anim.SetInteger("Weapon", 1);
            sword.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (AttackActive()) return;
            anim.SetInteger("Weapon", 2);
            sword.SetActive(false);
        }
    }

    public void LightAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton() && !ctx.performed) return;
        if (AttackActive()) return;
        if (GetComponent<PlayerState>().UpdateStamina(-25)) return;
        anim.SetTrigger("Attack");
        anim.SetBool("Heavy", false);

    }

    public void HeavyAttack(InputAction.CallbackContext ctx)
    {
        bool clicked = ctx.ReadValueAsButton();
        if (AttackActive()) return;
        PlayerState state = GetComponent<PlayerState>();
        if (state.Stamina < -40) return;
        anim.SetTrigger("Attack");
        anim.SetBool("Heavy", true);
    }

    private void Awake()
    {
        sword.SetActive(false);
        anim = GetComponent<Animator>();
    }
}
