using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO.Ports;

public class Serial_Detector : MonoBehaviour
{
    string[] ports;
    [SerializeField] private GameObject serialTesterPrefab;
    [SerializeField] Serial_State_UI_Manager serialStateUIManager;
    [SerializeField] public static string inputPort = "";
    [SerializeField] public static string outputPort = "";

    private int detectedPorts = 0;
    private bool isTextUpdated = false;

    // Start is called before the first frame update
    void Start(){
        // DontDestroyOnLoad(this);

        ports = SerialPort.GetPortNames();

        foreach(string port in ports){
            StartCoroutine(judgeSerialIsSelfActive(port));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTextUpdated && detectedPorts == ports.Length){
            isTextUpdated = true;
            serialStateUIManager.UpdateText(inputPort, outputPort);
        }
    }


    IEnumerator judgeSerialIsSelfActive(string port){
        Debug.Log("port:" + port);
        if(port == "COM1"){
            detectedPorts++;
            yield break;
        }

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

        detectedPorts++;
    }

    
    void OnDataReceived(string message){
        // Debug.Log(message);
    }
}
