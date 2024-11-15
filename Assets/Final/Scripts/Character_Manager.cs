using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext; 

public class Character_Manager : MonoBehaviour
{

    private static Character_Manager instance = null;

    [Header("PERSONAJES")]
    [SerializeField] GameObject ColoredChickPrefab;
    [SerializeField] GameObject CarlJhonsonPrefab;
    [SerializeField] GameObject HunkPrefab;

    GameObject _ChickChar;
    GameObject _CarlChar;
    GameObject _HunkChar;

    [Header("POSICIÓN INICIAL/SPAWN")]
    [SerializeField] Transform Spawn;

    Transform currentTransform;
    //int _charIndex;

    public static Character_Manager Instance
    {
        get
        {
            // test if the instance is null
            // if so, try to get it using FindObjectOfType
            if (instance == null)
                instance = FindFirstObjectByType<Character_Manager>();

            // if the instance is null again
            // create a new game object
            // attached the Singleton class on it
            // set the instance to the new attached Singleton
            // call don't destroy on load
            if (instance == null)
            {
                GameObject gObj = new GameObject();
                gObj.name = "Manager";
                instance = gObj.AddComponent<Character_Manager>();
                DontDestroyOnLoad(gObj);
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        _ChickChar = Instantiate(ColoredChickPrefab, Spawn);
        _CarlChar = Instantiate(CarlJhonsonPrefab, Spawn);
        _HunkChar = Instantiate(HunkPrefab, Spawn);
        //_charIndex = 0;
    }

    void Start()
    {
        _ChickChar.SetActive(false);
        _CarlChar.SetActive(false);
        currentTransform = _HunkChar.transform;
    }

    public void ChangeCharacter(int _charIndex){
            /*_charIndex++;
            if(_charIndex > 2) _charIndex = 0;*/
            _ChickChar.transform.position = currentTransform.position;
            _ChickChar.transform.rotation = currentTransform.rotation;

            _CarlChar.transform.position = currentTransform.position;
            _CarlChar.transform.rotation = currentTransform.rotation;

            _HunkChar.transform.position = currentTransform.position;
            _HunkChar.transform.rotation = currentTransform.rotation;
            
            switch(_charIndex){
                case 0:
                    _HunkChar.SetActive(true);
                    _ChickChar.SetActive(false);
                    _CarlChar.SetActive(false);
                    /*CAMBIAR LA UI*/
                    break;

                case 1:
                    _CarlChar.SetActive(true);
                    _ChickChar.SetActive(false);
                    _HunkChar.SetActive(false);
                    /*CAMBIAR LA UI*/
                    break;

                case 2:
                    _ChickChar.SetActive(true);
                    _CarlChar.SetActive(false);
                    _HunkChar.SetActive(false);
                    /*CAMBIAR LA UI*/
                    break;

                default:
                break;
            }
        
    }
}

