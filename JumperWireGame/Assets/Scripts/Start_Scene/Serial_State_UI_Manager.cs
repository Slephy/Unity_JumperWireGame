using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serial_State_UI_Manager : MonoBehaviour
{
    [SerializeField] Text serialState;
    [SerializeField] Button buttonStart;
    [SerializeField] Serial_Detector serialDetector;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void UpdateText(string inputPort, string outputPort){
        string display = "";
        bool isPortOK = true;
        if(inputPort == ""){
            inputPort = "None";
            isPortOK = false;
        }
        if(outputPort == ""){
            outputPort = "None";
            isPortOK = false;
        }

        display = "input: " + inputPort + ", output: " + outputPort;
        if(!isPortOK) serialState.color = new Color(1f, 0.5f, 0.5f);
        if(isPortOK) buttonStart.interactable = true;
        serialState.text = display;
    }
}
