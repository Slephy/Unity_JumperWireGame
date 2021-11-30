using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pipe_Prefab_manager : MonoBehaviour
{
    public Serial_Handler serialHandler;
    public GameObject pipe_prefab;
    public GameObject[,] pipe_inst = new GameObject[3, 3];
    
    // Start is called before the first frame update
    void Start()
    {
        // pipe_inst配列の初期化
        serialHandler.OnDataReceived += OnDataReceived;
        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 3; j++){
                pipe_inst[i, j] = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createPipe(int in_cube, int out_cube){
        if(pipe_inst[in_cube, out_cube] != null) return; // 既に作成されているなら何もしない
        GameObject p = Instantiate(pipe_prefab) as GameObject;
        pipe_inst[in_cube, out_cube] = p;
        float ix = (in_cube - 1) * 2;
        float ox = (out_cube - 1) * 2;

        // float theta = (float)(Math.Atan2(10, (ox - ix)) * 180 / Math.PI);
        float theta_rad = (float)(Math.Atan2(10, (ox - ix)));
        p.transform.position = new Vector3((ix+ox)/2, 1, 0);
        p.transform.localScale = new Vector3(0.3f, 5.0f / (float)Math.Sin(theta_rad), 0.3f);
        p.transform.Rotate(0.0f, 0.0f, 90.0f - (float)(theta_rad * 180.0f / Math.PI), Space.Self);
        
        Debug.Log("Create");
        Debug.Log(in_cube);
        Debug.Log(out_cube);
    }

    private void destroyPipe(int in_cube, int out_cube){
        if(pipe_inst[in_cube, out_cube] == null) return;
        else Destroy(pipe_inst[in_cube, out_cube]);
        
        Debug.Log("Destroy");
        Debug.Log(in_cube);
        Debug.Log(out_cube);
    }


    // void OnDataReceived(string message){
    //     var data = message.Split(
    //         new string[]{"\t"}, System.StringSplitOptions.None);

    //     try{
    //         int from = Int32.Parse(data[0]);
    //         int to = Int32.Parse(data[1]);
    //         bool isConnect = (data[2] == "1");
    //         if(isConnect) createPipe(from, to);
    //         else destroyPipe(from, to);
    //     }
    //     catch(System.Exception e){
    //         Debug.LogWarning(e.Message);
    //     }
    // }
    void OnDataReceived(string message){
        var data = message.Split(
            new string[]{"\t"}, System.StringSplitOptions.None);

        try{
            var data_int = Int32.Parse(data[0]);
            for(int i = 0; i < 3; i++){
                for(int j = 0; j < 3; j++){
                    bool isConnect = (data_int % 2 == 1);
                    if(isConnect) createPipe(i, j);
                    else destroyPipe(i, j);
                    data_int /= 2;
                }
            }
        }
        catch(System.Exception e){
            Debug.LogWarning(e.Message);
        }
    }
}
