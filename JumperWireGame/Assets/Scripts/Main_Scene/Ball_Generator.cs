using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Generator : MonoBehaviour
{
    [SerializeField] private GameObject ball_prefab;
    [SerializeField] private GameObject capsule_prefab;
    [SerializeField] private GameObject Debug_Time;
    private Text Debug_Time_Text;
    [SerializeField] private GameObject Test_Manager;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;
    [SerializeField] private Material red;

    const float GENERATE_X = -16.5f;
    const float GENERATE_Y = 2.7f;
    const float GENERATE_Z_MIN = -14.0f;
    const float GENERATE_Z_MAX = -2.0f;
    const int GENERATEPOS_MAX = 6;

    private bool isTest;
    StreamReader streamReader;
    double timeFromStart = 0;
    
    double time;
    string color;
    bool isCapsule;
    int _generate_pos;
    bool fileIsEnd = false;
    
    // Start is called before the first frame update
    void Start(){
        isTest = Test_Manager.GetComponent<Test_Manager>().CheckIfTest();
        Debug_Time_Text = Debug_Time.GetComponent<Text>();
        streamReader = new StreamReader(@"Assets/Resources/generate_pattern.txt");
        ReadNextLine();
    }

    // Update is called once per frame
    void Update(){
        timeFromStart += Time.deltaTime;
        if(timeFromStart >= time && !fileIsEnd){
            Debug.LogFormat("time: {0}, color: {1}, isCapsule: {2}", time, color, isCapsule);
            CreateBallOrCapsule(color, isCapsule, _generate_pos);
            ReadNextLine();
        }
        if(isTest){
            Renew_DebugTime();
        }
    }


    void ReadNextLine(){
        string str = streamReader.ReadLine();
        if(str == "END"){
            fileIsEnd = true;
            Debug.Log("FILE IS END");
            return;
        }

        string[] vstr = str.Split(' ');
        time = double.Parse(vstr[0]);
        color = vstr[1];
        isCapsule = (vstr[2] == "c" || vstr[2] == "C");
        _generate_pos = Int32.Parse(vstr[3]);
    }


    private void CreateBallOrCapsule(string color, bool isCapsule, int generatePos){
        // インスタンス化
        GameObject inst;
        if(isCapsule) inst = Instantiate(capsule_prefab) as GameObject;
        else inst = Instantiate(ball_prefab) as GameObject;

        // 位置の決定
        if(generatePos == -1){
            inst.transform.position = new Vector3(GENERATE_X, GENERATE_Y, UnityEngine.Random.Range(GENERATE_Z_MIN, GENERATE_Z_MAX));
        }
        else if(generatePos <= GENERATEPOS_MAX){
            float z = GENERATE_Z_MIN + (GENERATE_Z_MAX - GENERATE_Z_MIN) * generatePos / GENERATEPOS_MAX;
            inst.transform.position = new Vector3(GENERATE_X, GENERATE_Y, z);
        }
        else{
            Debug.Log("generatePos is invalid value: " + generatePos);
        }
        
        switch (color)
        {
            case "blue":
                inst.GetComponent<Renderer>().material = blue;
                break;
            case "green":
                inst.GetComponent<Renderer>().material = green;
                break;
            case "red":
                inst.GetComponent<Renderer>().material = red;
                break;
        }
    }

    
    void Renew_DebugTime(){
        Debug_Time_Text.text = timeFromStart.ToString("0.00");
    }
}
