//Authors
//Cian O'Toole <B00143633>
//Conor Donovan <B00134690>
//
//IMM
//
//Snake Game 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour{

    private float timer = 60f;
    public Text timerDisplay;

    void Start(){
        timerDisplay = GetComponent<Text>();
    }

    void Update(){
        timer -= Time.deltaTime;
        timerDisplay.text = timer.ToString("f2");
        if (timer <= 0){
            SceneManager.LoadScene(2);
        }
    }
}