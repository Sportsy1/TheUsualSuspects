using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolSeter : StateMachineBehaviour
{

    [Flags]
    enum ExecutionPhase
    {
        OnStateEnter = 2,
        OnStateExit = 4,
    }

    [SerializeField]
    private string paremeterName;
    [SerializeField]
    private bool value;
    [SerializeField]
    private ExecutionPhase phase;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!phase.HasFlag(ExecutionPhase.OnStateEnter)) return;
        animator.SetBool(paremeterName, value);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!phase.HasFlag(ExecutionPhase.OnStateExit)) return;
        animator.SetBool(paremeterName, value);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
