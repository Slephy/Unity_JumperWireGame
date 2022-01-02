using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_create_ball : MonoBehaviour
{
    [SerializeField] private GameObject ball_prefab;
    [SerializeField] private GameObject capsule_prefab;
    [SerializeField] private SE_Manager sePlayer;
    [SerializeField] private Test_Manager testManager;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    private bool isTest;
    private float[] NearGeneratePosZ = new float[3] {-3.3f, -8.0f, -12.5f}; 


    // Start is called before the first frame update
    void Start(){
        isTest = testManager.CheckIfTest();
    }

    // Update is called once per frame
    void Update(){
        if(isTest){
            if(Input.GetKeyDown(KeyCode.Alpha1)) CreateSomething("b");
            if(Input.GetKeyDown(KeyCode.Alpha2)) CreateSomething("r");
            if(Input.GetKeyDown(KeyCode.Alpha3)) CreateSomething("g");
        }
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
        if(generatePos == -1) inst.transform.position = new Vector3(-16.5f, 2.7f, Random.Range(-14.0f, -2.0f));
        else{
            inst.transform.position = new Vector3(-5.75f, 0, NearGeneratePosZ[generatePos]);
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

        sePlayer.Play(SE_Manager.kind.Generate_balls);
    }
}
