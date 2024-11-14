using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext; 

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] float LockDetectionRadius;
    [SerializeField] CinemachineVirtualCamera LockedVCam;
    [SerializeField] CinemachineFreeLook FreeVCam;
    [SerializeField] LayerMask Layer;
    [SerializeField] bool isLocked = false;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        LockedVCam.gameObject.SetActive(false);
    }
    public void LockCamera(CallbackContext ctx){
        if(ctx.performed){
            if (isLocked){
                
                UnlockCamera();
                return;
            }
            Collider[] TargetsInRange = Physics.OverlapSphere(transform.position, LockDetectionRadius, Layer);
            Debug.Log(TargetsInRange);
            if (TargetsInRange.Length==0){

                return;
            } 
            Transform BestTarget = TargetsInRange[0].transform;
            for(int i = 1; i<TargetsInRange.Length;i++)
            {
                float Distance = Vector3.Distance(transform.position, BestTarget.position);
                if(Distance > Vector3.Distance(TargetsInRange[i].transform.position, transform.position)){
                    BestTarget = TargetsInRange[i].transform;
                }
            }
            isLocked = true;
            anim.SetBool("Locked", true);
            LockedVCam.LookAt = BestTarget;
            LockedVCam.gameObject.SetActive(true);
            FreeVCam.gameObject.SetActive(false);
        }

    }

    void UnlockCamera(){
        isLocked = false;
        anim.SetBool("Locked", false);
        FreeVCam.gameObject.SetActive(true);
        LockedVCam.gameObject.SetActive(false);
    }

    /*#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, LockDetectionRadius);
    }
    #endif*/
}
