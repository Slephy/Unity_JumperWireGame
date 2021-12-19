using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using UnityEngine;

public class Ball_Generator : MonoBehaviour
{
    public GameObject ball_prefab;
    public GameObject capsule_prefab;
    public Material blue;
    public Material green;
    public Material red;

    StreamReader streamReader;
    double timeFromStart = 0;
    int time;
    string color;
    bool isCapsule;
    bool fileIsEnd = false;
    
    // Start is called before the first frame update
    void Start(){
        streamReader = new StreamReader(@"Assets/Resources/generate_pattern.txt");
        ReadNextLine();
    }

    // Update is called once per frame
    void Update(){
        timeFromStart += Time.deltaTime;
        if(timeFromStart >= time && !fileIsEnd){
            Debug.LogFormat("time: {0}, color: {1}, isCapsule: {2}", time, color, isCapsule);
            CreateBallOrCapsule(color, isCapsule, -1);
            ReadNextLine();
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
        time = Int32.Parse(vstr[0]);
        color = vstr[1];
        isCapsule = (vstr[2] == "c" || vstr[2] == "C");
    }


    private void CreateSomething(string color){
        bool isCapsule = Input.GetKey(KeyCode.C);
        int generatePos = -1;
        if(Input.GetKey(KeyCode.V)) generatePos = 0;
        else if(Input.GetKey(KeyCode.B)) generatePos = 1;
        else if(Input.GetKey(KeyCode.N)) generatePos = 2;

        CreateBallOrCapsule(color, isCapsule, generatePos);
    }


    private void CreateBallOrCapsule(string color, bool isCapsule, int generatePos){
        // インスタンス化
        GameObject inst;
        if(isCapsule) inst = Instantiate(capsule_prefab) as GameObject;
        else inst = Instantiate(ball_prefab) as GameObject;

        // 位置の決定
        if(generatePos == -1) inst.transform.position = new Vector3(-16.5f, 2.7f, UnityEngine.Random.Range(-14.0f, -2.0f));
        else{
            // inst.transform.position = new Vector3(-5.75f, 0, NearGeneratePosZ[generatePos]);
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
}
