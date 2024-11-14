using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chick_CharacterState : MonoBehaviour
{
    [SerializeField] private float breakResistance;
    public float BreakResistance {get; set;}
    [SerializeField] private int MaxHealth;
    private int healt;

    Animator animator;
    void Start()
    {
        healt = MaxHealth;
        animator = GetComponent<Animator>();
    }

    public void ChangeHealth(int deltaHealth){
        healt += deltaHealth;
        if(deltaHealth > 0){
            if(healt > MaxHealth) healt = MaxHealth;
            animator.SetTrigger("Healed");
        }
        if(healt <= 0){
            animator.SetTrigger("Die");
        }
    }

}
