using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVFXUpdater : MonoBehaviour
{
    [Header("Fire Fist VFX")]
    [Tooltip("The father of the system")] [SerializeField] ParticleSystem fireTornado;
    [Tooltip("All of the children Particle System")] [SerializeField] ParticleSystem[] fireTornadoChildren;
    [SerializeField] Material[] DissolveMaterials;
    [Tooltip("Fist and foot father")] [SerializeField] ParticleSystem FireFists;
    [SerializeField] float Duration;
    float _dissolveStrenght;

    [Header("Sword Fight")]
    [SerializeField] Material SwordMat;
    [SerializeField] float DurationS;

    public void InitFireParticles(){
        fireTornado.Play();
        StartCoroutine(DissolveTornado());
        TurnOnFist();
    }

    private IEnumerator DissolveTornado(){
        float elapsedTime = 0;
            while(elapsedTime < Duration){
                elapsedTime += Time.deltaTime;
                _dissolveStrenght = Mathf.Lerp(0.6f, 1, elapsedTime / Duration);
                DissolveMaterials[0].SetFloat("_Dissolving", _dissolveStrenght);
                _dissolveStrenght = Mathf.Lerp(0.65f, 1, elapsedTime / Duration);
                DissolveMaterials[1].SetFloat("_Dissolving", _dissolveStrenght);
                _dissolveStrenght = Mathf.Lerp(0.75f, 1, elapsedTime / Duration);
                DissolveMaterials[2].SetFloat("_Dissolving", _dissolveStrenght);
                _dissolveStrenght = Mathf.Lerp(0.6f, 1, elapsedTime / Duration);
                DissolveMaterials[3].SetFloat("_Dissolving", _dissolveStrenght);

                yield return null;
            }
 
    }

    public void TurnOnFist(){
        DissolveMaterials[3].SetFloat("_Dissolving", 0.65f);
        FireFists.Play();
    }

    public void TurnOffFX(){
        fireTornado.Stop();
        FireFists.Stop();
    }

    public void ToggleSword(float init, float finish, GameObject sword){
        Material mat = sword.GetComponent<Renderer>().material;
        StartCoroutine(DissolveSword(init, finish, mat));
    }

    private IEnumerator DissolveSword(float init, float finish, Material mat){
        float elapsedTime = 0;
        while(elapsedTime < Duration){
            elapsedTime += Time.deltaTime;
            _dissolveStrenght = Mathf.Lerp(init, finish, elapsedTime/DurationS);
            mat.SetFloat("_AlphaCliping", _dissolveStrenght);
            yield return null;
        }
    }

    
}
