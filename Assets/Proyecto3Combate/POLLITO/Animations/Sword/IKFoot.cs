using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class IKFoot : StateMachineBehaviour
{
    [SerializeField] LayerMask detectionLayers;
    [SerializeField] float extraHeight = 0.07f;

    [SerializeField] FloatDampener leftFootOffsetDampener;
    [SerializeField] FloatDampener rightFootOffsetDampener;

    [SerializeField] QuaternionDampener leftFootRotationDampener;
    [SerializeField] QuaternionDampener rightFootRotationDampener;
    void FindSurfaceFromFoot(Animator animator,AvatarIKGoal goal, 
    out float surfaceOffset, 
    out Vector3 footPosition, 
    out Vector3 surfaceNormal){

        if((int)goal>1) throw new ArgumentException("Only Works for feet");
        Transform foot = animator.GetBoneTransform((HumanBodyBones)(int)goal+5);
        Vector3 rayOrigin = foot.position + Vector3.up;
        Vector3 rayDirection = Vector3.down;
        bool detected = Physics.SphereCast(rayOrigin, .1f, rayDirection, out RaycastHit hit, 1+ extraHeight *2, detectionLayers);
        surfaceOffset = Mathf.Max(0, (hit.point.y - foot.position.y)+ extraHeight);
        footPosition = foot.position;
        surfaceNormal = hit.normal;

    }

    Quaternion SolveFootRotation(Animator animator, AvatarIKGoal goal, Vector3 surfaceNormal){

        
        Vector3 axis = Vector3.Cross(Vector3.up, surfaceNormal);
        float angle = Vector3.Angle(Vector3.up, surfaceNormal);
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        return animator.GetIKRotation(goal) * rotation;
    }
    
    public override void OnStateIK(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

        FindSurfaceFromFoot(animator, AvatarIKGoal.LeftFoot, out float leftFootOffset, out Vector3 leftFootPosition, out Vector3 leftFootNormal);
        FindSurfaceFromFoot(animator, AvatarIKGoal.RightFoot, out float rightFootOffset, out Vector3 rightFootPosition, out Vector3 rightFootNormal);

        leftFootOffsetDampener.TargetValue = leftFootOffset;
        rightFootOffsetDampener.TargetValue = rightFootOffset;

        Quaternion leftFootRotation = SolveFootRotation(animator, AvatarIKGoal.LeftFoot, leftFootNormal);
        Quaternion rightFootRotation = SolveFootRotation(animator, AvatarIKGoal.LeftFoot, rightFootNormal);

        leftFootRotationDampener.TargetValue = leftFootRotation;
        rightFootRotationDampener.TargetValue = rightFootRotation;

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPosition + Vector3.up * leftFootOffsetDampener.CurrentValue);
        animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPosition + Vector3.up * rightFootOffsetDampener.CurrentValue);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotationDampener.CurrentValue);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotationDampener.CurrentValue);
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = animator.rootPosition;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        leftFootOffsetDampener.Update();
        rightFootOffsetDampener.Update();
        leftFootRotationDampener.Update();
        rightFootRotationDampener.Update();
    }
}
