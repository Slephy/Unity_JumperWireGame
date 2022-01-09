using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;

public class Serial_Detector : MonoBehaviour
{
    string[] ports;
    [SerializeField] private GameObject serialTesterPrefab;
    [SerializeField] string inputPort;
    [SerializeField] string outputPort;
    // Start is called before the first frame update
    void Start(){
        ports = SerialPort.GetPortNames();

        foreach(string port in ports){
            StartCoroutine(judgeSerialIsSelfActive(port));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator judgeSerialIsSelfActive(string port){
        Debug.Log("port:" + port);
        if(port == "COM1") yield break;

        var serialTester = Instantiate(serialTesterPrefab) as GameObject;
        serialTester.GetComponent<Serial_name>().portName = port;

        serialTester.AddComponent<Serial_Handler>();
        var serialHandler = serialTester.GetComponent<Serial_Handler>();

        serialHandler.OnDataReceived += OnDataReceived;

        if(!serialHandler.isOpen()){
            Debug.LogErrorFormat("Port{0} can't be opened", port);
        }


        yield return new WaitForSeconds(2.0f);

        if(serialHandler.isSerialActive) inputPort = port;
        else outputPort = port;
        Destroy(serialTester);
    }

    
    void OnDataReceived(string message){
        // Debug.Log(message);
    }
}
