using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// please set arduino code "test_29"

public class test_cube_serial : MonoBehaviour
{
    public int PortNumber;
    public Serial_Handler serial_Handler;
    // public GameObject[] Port_Cubes = new GameObject[2];
    public GameObject Port_Cube;


    // Start is called before the first frame update
    void Start()
    {
        serial_Handler.OnDataReceived += OnDataReceived;
        Port_Cube = GameObject.Find("Port" + PortNumber);
        if(Port_Cube == null) Debug.Log("Port" + PortNumber + "is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeCubeVisibility(){
        Debug.Log("change cube visibility start");
        if(Port_Cube.activeSelf) Port_Cube.SetActive(false);
        else Port_Cube.SetActive(true);
        Debug.Log("change cube visibility end");
    }

    void OnDataReceived(string message){
        var data = message.Split(
            new string[]{"\t"}, System.StringSplitOptions.None);

            try{
                var data_int = Int32.Parse(data[0]);
                // Port2 -> 0, Port3 -> 1を送信してくる 
                ChangeCubeVisibility();
            }
            catch(System.Exception e){
                Debug.LogWarning(e.Message);
            }
    }
}
