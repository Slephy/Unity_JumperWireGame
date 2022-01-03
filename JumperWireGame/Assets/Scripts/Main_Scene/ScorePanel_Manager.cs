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

    void Start(){
        scorePanel.gameObject.SetActive(false);
        btnRetry.gameObject.SetActive(false);
        btnHome.gameObject.SetActive(false);
        yourScore.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ActivateScorePanel(int score){
        yield return new WaitForSeconds(1.0f);

        scorePanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        
        yourScore.gameObject.SetActive(true);
        sePlayer.Play(SE_Manager.kind.UI_don);
        yield return new WaitForSeconds(0.5f);
        
        _score.text = score.ToString();
        _score.gameObject.SetActive(true);
        sePlayer.Play(SE_Manager.kind.UI_dodon);
        yield return new WaitForSeconds(0.5f);
        
        btnRetry.gameObject.SetActive(true);
        btnHome.gameObject.SetActive(true);
    }
}
