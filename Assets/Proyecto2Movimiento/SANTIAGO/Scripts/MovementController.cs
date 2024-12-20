using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using CallbackContent = UnityEngine.InputSystem.InputAction.CallbackContext; 

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class MovementController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private VectorDamper_Pollito motionVector = new VectorDamper_Pollito(true);


    private int VelXid; 
    private int VelYid;

    public void Pause(CallbackContent ctx){
        UIManager.Instance.Pause();
    }

    public void Move(CallbackContent ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        if(direction != Vector2.zero) anim.SetBool("isIdling", false);
        else {
            anim.SetBool("isIdling", true);
        } 
        motionVector.TargetValue = direction; 
    }

    public void ToggleSprint(CallbackContent ctx)
    {
        bool val = ctx.ReadValueAsButton();
        motionVector.Clamp = !val; 
    }

    public void Jump(CallbackContent ctx){
        if(anim.GetBool("isJumping")) return;
        bool val = ctx.performed;
        if (val) anim.SetTrigger("Jump");
    }

    public void onDodge(CallbackContent ctx) {
        if(ctx.performed){
            anim.SetTrigger("Dodge");
        }
    }

    private void Awake()
    {
        if(cameraTransform == null) cameraTransform = Camera.main.transform;
        anim = GetComponent<Animator>();
        VelXid = Animator.StringToHash("VX");
        VelYid = Animator.StringToHash("VY"); 
        anim.SetBool("isIdling", true);
    }

    private void Update()
    {
        motionVector.Update();
        Vector2 direction = motionVector.CurrentValue; 
        anim.SetFloat(VelXid, direction.x);
        anim.SetFloat(VelYid, direction.y);
    }

    public void OnAnimatorMove()
    {
        float interpolator = Mathf.Abs(Vector3.Dot(cameraTransform.forward, transform.up)); 
        Vector3 forward = Vector3.Lerp(cameraTransform.forward, cameraTransform.up, interpolator);
        Vector3 projected = Vector3.ProjectOnPlane(forward, transform.up);
        Quaternion rotation = Quaternion.LookRotation(projected, transform.up); 
        anim.rootRotation = Quaternion.Slerp(anim.rootRotation, rotation, motionVector.CurrentValue.magnitude);
        anim.ApplyBuiltinRootMotion();
    }

}
