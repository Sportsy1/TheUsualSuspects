using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class OnEntryBoolChecker : StateMachineBehaviour
{
    [SerializeField] string[] parameterNames;
    [Tooltip("Has to be the same size of Parameter names, this will set the values in order, so be sure of putting the name of the parameter in the same array position of the value you want")] 
    [SerializeField] bool[] parameterValues;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for(int i = 0; i<parameterNames.Length; i++){
            animator.SetBool(parameterNames[i], parameterValues[i]);
        }
        /*animator.SetBool("isJumping", true);
        animator.SetBool("canChange", false);*/
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for(int i = 0; i<parameterNames.Length; i++){
            animator.SetBool(parameterNames[i], !parameterValues[i]);
        }
        /*animator.SetBool("isJumping", false);
        animator.SetBool("canChange", true);*/
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
