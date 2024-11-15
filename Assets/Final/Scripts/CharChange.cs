using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class CharChange : MonoBehaviour
{
    [SerializeField] int nextChar;
    [SerializeField] int prevChar;

    [SerializeField] float CDT;
    float Cooldown;

    public void ChangeCharacter(CallbackContext ctx){
        Cooldown -= Time.deltaTime;
        if(Cooldown > 0) return;
        float CharIndex = ctx.ReadValue<float>();
        if(CharIndex < 0){
            Character_Manager.Instance.ChangeCharacter(nextChar);
        }
        else{
            Character_Manager.Instance.ChangeCharacter(prevChar);
        }
        Cooldown = CDT;
    }
}
