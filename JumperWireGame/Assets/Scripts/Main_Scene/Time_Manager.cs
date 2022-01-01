using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_Manager : MonoBehaviour
{
    [SerializeField] private Test_Manager testManager;
    [SerializeField] private Text debugTimeText;
    [SerializeField] private float START_TIME = 0.0f; // タイマーの初期値
    [SerializeField] private float now;

    private bool isTest;

    void Start(){
        now = START_TIME;
        isTest = testManager.CheckIfTest();
    }

    // Update is called once per frame
    void Update(){
        now += Time.deltaTime;

        if(isTest){
            Renew_DebugTime();
        }
    }

    public float GetTime(){
        return now;
    }

    void Renew_DebugTime(){
        debugTimeText.text = now.ToString("0.00");
    }
}
