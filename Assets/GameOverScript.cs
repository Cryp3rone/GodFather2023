using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    private GameObject menuPause;
    public bool isPaused;


    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = "Score : " + score.ToString();
    }

    private void Start() {
        menuPause = transform.GetChild(0).gameObject;
        menuPause.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                pauseOFF();
            }
            else{
                pauseON();
            }
        }
    }

    public void pauseON(){
        Time.timeScale = 0f;
        menuPause.SetActive(true);
        isPaused = true;
    }

    public void pauseOFF(){
        Time.timeScale = 1f;
        menuPause.SetActive(false);
        isPaused = false;
    }
}
