using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScorePanel_Manager : MonoBehaviour
{
    [SerializeField] private SE_Manager sePlayer;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Button btnRetry;
    [SerializeField] private Button btnHome;
    [SerializeField] private TextMeshProUGUI yourScore;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI message;

    void Start(){
        scorePanel.gameObject.SetActive(false);
        btnRetry.gameObject.SetActive(false);
        btnHome.gameObject.SetActive(false);
        yourScore.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ActivateScorePanel(int score, int ballQuantity){
        yield return new WaitForSeconds(1.0f);

        scorePanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        
        yourScore.gameObject.SetActive(true);
        sePlayer.Play(SE_Manager.kind.result_don);
        yield return new WaitForSeconds(1.0f);
        
        _score.text = score.ToString();
        _score.gameObject.SetActive(true);

        float getPercent = 1.0f * score / ballQuantity; 
        Debug.Log("Your getPercent = " + score);
        Debug.Log("Your ballQuantity = " + ballQuantity);
        Debug.Log("Your getPercent = " + getPercent);
        // Perfect
        if(score == ballQuantity){
            sePlayer.Play(SE_Manager.kind.result_perfect);
            message.text = "Perfect!!";
        }
        // Great
        else if(getPercent >= 0.7f){
            sePlayer.Play(SE_Manager.kind.result_great);
            message.text = "Great!";
        }
        // Good
        else if(getPercent >= 0.3f){
            sePlayer.Play(SE_Manager.kind.result_good);
            message.text = "Good!";
        }
        // Bad
        else{
            sePlayer.Play(SE_Manager.kind.result_bad);
            message.text = "Try Again!";
        }
        message.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        btnRetry.gameObject.SetActive(true);
        btnHome.gameObject.SetActive(true);
    }
}
