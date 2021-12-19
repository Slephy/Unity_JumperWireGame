using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private Text debugScoreText;
    // public GameObject Debug_Score;
    [SerializeField] private Test_Manager testManager;
    private bool isTest;
    // Start is called before the first frame update
    void Start()
    {
        isTest = testManager.CheckIfTest();
        ResetScore();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.F)){
            AddScore();
        }
    }

    public void AddScore(){
        this.score++;
        Renew_DebugScore();
    }

    public void ResetScore(){
        this.score = 0;
        Renew_DebugScore();
    }

    private void Renew_DebugScore(){
        if(isTest){
           debugScoreText.text = "score: " + score.ToString();
        }
    }
}
