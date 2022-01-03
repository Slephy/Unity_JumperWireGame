using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Remain_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainText;
    [SerializeField] private Image background;


    void Start(){
        remainText.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
    }

    public void initRemain(int ball_quantity){
        string s = "Remain:" + " " + ball_quantity.ToString();
        remainText.text = s;
    }

    public void renewRemain(int remain){
        string s = "Remain:" + " " + remain.ToString();
        remainText.text = s;
    }
}
