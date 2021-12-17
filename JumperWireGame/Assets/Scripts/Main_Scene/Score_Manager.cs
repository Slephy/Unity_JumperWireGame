using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    [SerializeField] private int score;
    public GameObject Debug_Score;
    public GameObject Test_Manager;
    // Start is called before the first frame update
    void Start()
    {
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
        if(Test_Manager.GetComponent<Test_Manager>().CheckIfTest()){
           Debug_Score.GetComponent<Text>().text = score.ToString();
        }
    }
}
