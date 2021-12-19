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
    [SerializeField] private Text Debug_Time_Text;
    [SerializeField] private Test_Manager test_Manager;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;
    [SerializeField] private Material red;

    const float GENERATE_X = -16.5f;
    const float GENERATE_Y = 2.7f;
    const float GENERATE_Z_MIN = -14.0f;
    const float GENERATE_Z_MAX = -2.0f;
    const int GENERATEPOS_MAX = 5;

    private bool isTest;
    StreamReader streamReader;
    double timeFromStart = 0;


    struct Generate_Info
    {
        public double time;
        public int pos;
        public string color;
        public bool isCapsule;
    }
    Generate_Info info;
    bool fileIsEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        // テストモードでの処理
        isTest = test_Manager.CheckIfTest();

        // 生成パターンファイルの読み込み
        streamReader = new StreamReader(@"Assets/Resources/generate_pattern.txt");
        ReadNextLine();
    }

    // Update is called once per frame
    void Update()
    {
        timeFromStart += Time.deltaTime;
        if (timeFromStart >= info.time && !fileIsEnd)
        {
            Debug.LogFormat("time: {0}, color: {1}, isCapsule: {2}", info.time, info.color, info.isCapsule);
            CreateBallOrCapsule(info.color, info.isCapsule, info.pos);
            ReadNextLine();
        }
        if (isTest)
        {
            Renew_DebugTime();
        }
    }


    void ReadNextLine()
    {
        string str = streamReader.ReadLine();
        if (str == "END")
        {
            fileIsEnd = true;
            Debug.Log("FILE IS END");
            return;
        }

        string[] vstr = str.Split(' ');
        info.time = double.Parse(vstr[0]);
        info.pos = Int32.Parse(vstr[1]);
        info.color = vstr[2];
        info.isCapsule = (vstr[3] == "c" || vstr[3] == "C");
    }


    private void CreateBallOrCapsule(string color, bool isCapsule, int generatePos)
    {
        // インスタンス化
        GameObject inst;
        if (isCapsule) inst = Instantiate(capsule_prefab) as GameObject;
        else inst = Instantiate(ball_prefab) as GameObject;

        // 位置の決定
        if (generatePos == -1)
        {
            inst.transform.position = new Vector3(GENERATE_X, GENERATE_Y, UnityEngine.Random.Range(GENERATE_Z_MIN, GENERATE_Z_MAX));
        }
        else if (generatePos <= GENERATEPOS_MAX)
        {
            float z = GENERATE_Z_MIN + (GENERATE_Z_MAX - GENERATE_Z_MIN) * generatePos / GENERATEPOS_MAX;
            inst.transform.position = new Vector3(GENERATE_X, GENERATE_Y, z);
        }
        else
        {
            Debug.Log("generatePos is invalid value: " + generatePos);
        }

        switch (color)
        {
            case "b":
                inst.GetComponent<Renderer>().material = blue;
                break;
            case "g":
                inst.GetComponent<Renderer>().material = green;
                break;
            case "r":
                inst.GetComponent<Renderer>().material = red;
                break;
        }
    }


    void Renew_DebugTime()
    {
        Debug_Time_Text.text = timeFromStart.ToString("0.00");
    }
}
