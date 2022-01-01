using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pipe_Generator : MonoBehaviour
{
    [SerializeField] private Serial_Handler serialHandler;
    [SerializeField] private Test_Manager testManager;
    [SerializeField] private SE_Manager sePlayer;
    private bool isTest;
    private const float PIPE_GENERATE_DURATION = 0.15f;
    [SerializeField] private Material glass;
    [SerializeField] private Material grayGlass;


    private GameObject[,,] pipeParts = new GameObject[3, 3, 4];
    private Renderer[,,] pipeRenderer = new Renderer[3, 3, 4];
    private pipeState[,] pipeStates = new pipeState[3, 3];
    private int[,] pipeInterruptedBy = new int[3, 3]; // -1:妨げられていない, 0-2:このパイプに妨げられている
    
    private string[,] pipeName = {{"pipe_1(Blue)", "pipe_2(Green-1)", "pipe_3(Red)"},
                                  {"pipe_2(Blue)", "pipe_1(Green)",   "pipe_2(Red)"},
                                  {"pipe_3(Blue)", "pipe_2(Green-2)", "pipe_1(Red)"}};

    private enum pipeState{
        None,
        Generating,
        Generated,
        Destroying,
    }

    // Start is called before the first frame update
    void Start()
    {
        // serialHandlerの初期化
        serialHandler.OnDataReceived += OnDataReceived;

        // isTestの初期化
        isTest = testManager.CheckIfTest();

        // pipePartsの初期化
        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 3; j++){
                Transform pipe = GameObject.Find(pipeName[i, j]).GetComponent<Transform>();
                pipeStates[i, j] = pipeState.None;
                pipeInterruptedBy[i, j] = -1;

                for (int k = 0; k < 4; k++){
                    char pipe_group = pipeName[i, j][5];
                    string child_index = (k+1).ToString();
                    string childName = "pipe_" + pipe_group + "-" + child_index;
                    // Debug.Log(childName);
                    pipeParts[i, j, k] = pipe.Find(childName).gameObject;
                    pipeRenderer[i, j, k] = pipeParts[i, j, k].GetComponent<Renderer>();
                    pipeRenderer[i, j, k].material = glass;
                    pipeParts[i, j, k].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update(){
        // // デバッグ用 テンキーでパイプ生成
        // if(isTest){
        //     for (int i = 0; i < 3; i++){
        //         for (int j = 0; j < 3; j++){
        //             if(Input.GetKeyDown((KeyCode)(KeyCode.Keypad1 + (3*i + j)))){
        //                 StartCoroutine(CreatePipeGrad(i, j));
        //                 // CreatePipe(i, j);
        //             }
        //         }
        //     }
        // }
    }

    // void CreatePipe(int from, int to){
    //     for (int i = 0; i < 4; i++){
    //         ChangeActiveState(pipeParts[from, to, i]);
    //     }
    // }

    // void ChangeActiveState(GameObject g){
    //     g.SetActive(!g.activeSelf);
    // }

    void OnDataReceived(string message){
        int data = Int32.Parse(message);
        // Debug.Log(message);
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                bool isActive = ((data & (1 << (3*i + j))) > 0);
                var ps = pipeStates[i, j];

                // パイプを生成
                if(isActive && (ps == pipeState.None || ps == pipeState.Destroying)){ 
                    pipeStates[i, j] = pipeState.Generating;
                    pipeInterruptedBy[i, j] = -1;

                    // すでに生成されているパイプを妨げる
                    for(int k = 0; k < 3; k++){
                        if(k != j && pipeInterruptedBy[i, k] == -1 && 
                        (pipeStates[i, k] == pipeState.Generating || pipeStates[i, k] == pipeState.Generated))
                        {
                            pipeInterruptedBy[i, k] = j;
                            StartCoroutine(InterruptPipe(i, k));
                        }
                    }
                    StartCoroutine(CreatePipeGrad(i, j));
                }

                // パイプを破壊
                else if(!isActive && (ps == pipeState.Generated || ps == pipeState.Generating)){
                    pipeStates[i, j] = pipeState.Destroying;

                    // 自分が妨げていたパイプに、自分のInterrupted情報をコピーする
                    for(int k = 0; k < 3; k++){
                        if(k != j && pipeInterruptedBy[i, k] == j){
                            pipeInterruptedBy[i, k] = pipeInterruptedBy[i, j];
                            if(pipeInterruptedBy[i, k] == -1) StartCoroutine(ResumeInterruptedPipe(i, k));
                        }
                    }

                    pipeInterruptedBy[i, j] = -1;
                    StartCoroutine(DestroyPipeGrad(i, j));
                }
            }
        }
    }

    IEnumerator CreatePipeGrad(int from, int to){
        for(int i = 0; i < 4; i++){
            pipeParts[from, to, i].SetActive(true);
            if(pipeInterruptedBy[from, to] == -1) pipeRenderer[from, to, i].material = glass;
            else pipeRenderer[from, to, i].material = grayGlass;
            sePlayer.Play((int)SE_Manager.kind.Generate_pipe);
            if(i != 3) yield return new WaitForSeconds(PIPE_GENERATE_DURATION);
        }
        if(pipeStates[from, to] == pipeState.Generating) pipeStates[from, to] = pipeState.Generated;
    }


    IEnumerator DestroyPipeGrad(int from, int to){
        for(int i = 0; i < 4; i++){
            if(!pipeParts[from, to, i].activeSelf) continue; // パイプが妨げられてすでに破壊されていたとき

            pipeParts[from, to, i].SetActive(false);
            sePlayer.Play((int)SE_Manager.kind.Destroy_pipe);
            if(i != 3) yield return new WaitForSeconds(PIPE_GENERATE_DURATION);
        }
        if(pipeStates[from, to] == pipeState.Destroying) pipeStates[from, to] = pipeState.None;
    }


    IEnumerator InterruptPipe(int from, int to){
        pipeParts[from, to, 0].SetActive(false);
        for(int i = 1; i < 4; i++){
            pipeRenderer[from, to, i].material = grayGlass;
        }
        yield return null;
    }


    IEnumerator ResumeInterruptedPipe(int from, int to){
        pipeParts[from, to, 0].SetActive(true);
        for(int i = 1; i < 4; i++){
            pipeRenderer[from, to, i].material = glass;
        }
        yield return null;
    }
}
