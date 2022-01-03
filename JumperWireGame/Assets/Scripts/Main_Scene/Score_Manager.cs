using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int generatedBall;
    [SerializeField] private int destroyedBall;
    [SerializeField] private int ballQuantity = int.MaxValue;
    [SerializeField] private Text debugScoreText;
    [SerializeField] private ScorePanel_Manager scorePanelManager;
    [SerializeField] private Test_Manager testManager;
    private bool isTest;
    
    // Start is called before the first frame update
    void Start(){
        isTest = testManager.CheckIfTest();
        Reset();
    }

    // Update is called once per frame
    void Update(){
        if(isTest){
            if(Input.GetKeyDown(KeyCode.F)){
                AddScore();
            }
        }
    }

    public void InitBallQuantity(int q){
        ballQuantity = q;
    }

    public void AddScore(){
        score++;
        Renew_DebugScore();
    }

    public void AddGeneratedBall(){
        generatedBall++;
    }

    public void AddDestroyedBall(){
        destroyedBall++;
        if(destroyedBall == ballQuantity){
            Debug.Log("CALLED ActivateScorePanal");
            StartCoroutine(scorePanelManager.ActivateScorePanel(score, ballQuantity));
        }
    }

    public void Reset(){
        score = 0;
        generatedBall = 0;
        destroyedBall = 0;
        Renew_DebugScore();
    }

    private void Renew_DebugScore(){
       debugScoreText.text = "score: " + score.ToString();
    }

}
