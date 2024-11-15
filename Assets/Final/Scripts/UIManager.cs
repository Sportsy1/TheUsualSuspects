using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Slider HealthBar;
    [SerializeField] TextMeshProUGUI EnemiesText;
    [SerializeField] GameObject WinPanel;
    int enemiesKilled = 0;

    void Start()
    {
        Instructions.SetActive(true);
        PauseMenu.SetActive(false);
        WinPanel.SetActive(false);
    }
    public void ToggleInstructions(){
        Instructions.SetActive(false);
    }

    public void UpdateHealth(float value){
        HealthBar.value = value;
    }

    public void UpdateEnemyCount(){
        enemiesKilled++;
        EnemiesText.text = enemiesKilled.ToString()+"/3";
        if(enemiesKilled ==3) Win();
    }

    public void Resume(){
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
    }

    public void Pause(){
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void Restart(){
        Time.timeScale = 1.0f;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        Start();
    }

    private void Win(){
        WinPanel.SetActive(true);
    }

}
