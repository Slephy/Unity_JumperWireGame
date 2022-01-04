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

        if(ports.Length != 2){
            Debug.LogWarningFormat("Available ports is not 2 ports. Actually {0}", ports.Length);
            return;
        }

        foreach(string port in ports){
            Debug.Log("port:" + port);
            var serialTester = Instantiate(serialTesterPrefab) as GameObject;
            serialTester.GetComponent<Serial_name>().portName = port;

            serialTester.AddComponent<Serial_Handler>();
            var serialHandler = GetComponent<Serial_Handler>();
            if(!serialHandler.isOpen()){
                Debug.LogErrorFormat("Port{0} can't be opened", port);
                continue;
            }
            if(serialHandler.isDataArrived()) inputPort = port;
            else outputPort = port;
            // serialHandler = null;
            Destroy(serialTester);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
