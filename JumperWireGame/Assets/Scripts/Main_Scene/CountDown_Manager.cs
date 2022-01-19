using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown_Manager : MonoBehaviour
{
    [SerializeField] private Time_Manager timeManager;
    [SerializeField] private SE_Manager sePlayer;
    [SerializeField] private TextMeshProUGUI CountDownText;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public IEnumerator DoCountDown(){
        CountDownText.gameObject.SetActive(true);
        for(int i = 3; i >= 0; i--){
            if(i == 0){
                CountDownText.text = "START";
                sePlayer.Play(SE_Manager.kind.Count_GameStart);
            }
            else{
                CountDownText.text = i.ToString();
                sePlayer.Play(SE_Manager.kind.Count_Down);
            }
            yield return new WaitForSeconds(1.0f);
        }
        CountDownText.gameObject.SetActive(false);
    }

}
