using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    private GameObject menuPause;
    public bool isPaused;
    private bool isGameOver = false;
    private bool canReplay = true;

    public void Setup(int score)
    {
        isGameOver = true;
        canReplay = false;
        pauseON();
    }

    private void Start() {
        menuPause = transform.GetChild(0).gameObject;
        pauseON();
    }

    public void pauseON(){
        Time.timeScale = 0f;
        menuPause.SetActive(true);
        isPaused = true;
        if(isGameOver){
            menuPause.transform.GetChild(0).gameObject.SetActive(true);
            menuPause.transform.GetChild(1).gameObject.SetActive(false);
            menuPause.transform.GetChild(2).gameObject.SetActive(false);
            StartCoroutine(WaitRestart());
        }
        else{
            menuPause.transform.GetChild(0).gameObject.SetActive(false);
            menuPause.transform.GetChild(1).gameObject.SetActive(false);
            menuPause.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void pauseOFF(){
        Time.timeScale = 1f;
        menuPause.SetActive(false);
        isPaused = false;
    }

    private void Update() {
        if (isPaused && Input.GetKeyDown(KeyCode.P) && canReplay){
            pauseOFF();
        }
    }

    IEnumerator WaitRestart(){
        yield return new WaitForSeconds(2f);
        canReplay = true;
        menuPause.transform.GetChild(0).gameObject.SetActive(false);
        menuPause.transform.GetChild(1).gameObject.SetActive(true);
        menuPause.transform.GetChild(2).gameObject.SetActive(false);

    }
}
