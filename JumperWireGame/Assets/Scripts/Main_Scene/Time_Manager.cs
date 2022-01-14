using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_Manager : MonoBehaviour
{
    [SerializeField] private Test_Manager testManager;
    [SerializeField] private Text debugTimeText;
    [SerializeField] private CountDown_Manager countDownManager;
    [SerializeField] private Animator animator;

    [SerializeField] private float now = 0.0f;
    private float START_TIME = -4.0f; // タイマーの初期値
    private const float START_COUNTDOWN = -3.0f;
    private const float START_GAME = 0.0f;
    private bool countdowned = false;
    private bool gameStarted = false;
    private bool isTest;
    

    void Start(){
        if(Demo_Manager.isDemo) START_TIME = -1000000.0f;
        now = START_TIME;
        isTest = testManager.CheckIfTest();
    }

    // Update is called once per frame
    void Update(){
        now += Time.deltaTime;
        Renew_DebugTime();

        if(!countdowned && now >= START_COUNTDOWN){
            countdowned = true;
            StartCoroutine(countDownManager.DoCountDown());
        }

        if(!gameStarted && now >= START_GAME){
            gameStarted = true;
            animator.SetTrigger("GameStart");
        }
    }

    public float GetTime(){
        return now;
    }

    void Renew_DebugTime(){
        debugTimeText.text = now.ToString("0.00");
    }
}
