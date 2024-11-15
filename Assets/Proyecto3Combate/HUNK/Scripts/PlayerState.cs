using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private float maxStamina = 100f;
    [SerializeField]
    private float staminaRegen = 10f;

    [SerializeField]
    private float stamina;

    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float currentHealth;

    private void Update()
    {
        stamina += Time.deltaTime * staminaRegen;
        stamina = Mathf.Min(stamina, maxStamina);
    }

    private void Awake()
    {
        stamina = maxStamina;
        currentHealth = maxHealth;
    }

    public bool UpdateStamina(float staminaDelta)
    {
        if(GetComponent<RootMotionNav>() != null) return false;
        if (stamina >= Mathf.Abs(staminaDelta))
        {
            stamina += staminaDelta;
            return true;
        }
        return false;
    }

    public bool UpdateHealth(float healthDelta)
    {
        if (currentHealth > healthDelta)
        {
            currentHealth += healthDelta;
            return true;
        }

        return false;
    }

    public float Stamina => stamina;

}
