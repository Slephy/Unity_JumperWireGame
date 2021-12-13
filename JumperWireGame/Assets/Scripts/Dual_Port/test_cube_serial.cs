using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_cube_serial : MonoBehaviour
{
    public int PortNumber;
    public Serial_Handler serial_Handler;
    // public GameObject[] Port_Cubes = new GameObject[2];
    public GameObject Port_cube;


    // Start is called before the first frame update
    void Start()
    {
        serial_Handler.OnDataReceived += OnDataReceived;
        for (int i = 0; i < 2; i++){
            Port_Cubes[i] = GameObject.Find("Port" + i);
            if(Port_Cubes == null) Debug.Log("Port" + (i+2) + "is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeCubeVisibility(int index){
        GameObject cube = Port_Cubes[index];
        if(cube.activeSelf) cube.SetActive(true);
        else cube.SetActive(false);
    }

    void OnDataReceived(string message){
        var data = message.Split(
            new string[]{"\t"}, System.StringSplitOptions.None);

            try{
                var data_int = Int32.Parse(data[0]);
                // Port2 -> 0, Port3 -> 1を送信してくる 
                ChangeCubeVisibility(data_int);
            }
            catch(System.Exception e){
                Debug.LogWarning(e.Message);
            }
    }
}
