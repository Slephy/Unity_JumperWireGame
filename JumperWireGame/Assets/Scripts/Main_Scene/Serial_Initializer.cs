using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serial_Initializer : MonoBehaviour
{
    [SerializeField] private GameObject serialTesterPrefab;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public Serial_Handler InitializeSerialHandler(bool forInput){
        var serialObject = Instantiate(serialTesterPrefab) as GameObject;

        string port, objectName;
        if(forInput){
            port = Serial_Detector.inputPort;
            objectName = "__Serial Handler Input";
        }
        else{
            port = Serial_Detector.outputPort;
            objectName = "__Serial Handler Output";
        }

        serialObject.name = objectName;
        serialObject.GetComponent<Serial_name>().portName = port;


        serialObject.AddComponent<Serial_Handler>();
        var serialHandler = serialObject.GetComponent<Serial_Handler>();
        return serialHandler;
    }
}
