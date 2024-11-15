using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOFF : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.ToggleInstructions();
        Destroy(this.gameObject);
    }
}
