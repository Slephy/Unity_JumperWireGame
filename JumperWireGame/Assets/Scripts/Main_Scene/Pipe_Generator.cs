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
    
    private string[,] pipeName = {{"pipe_1(Blue)", "pipe_2(Green-1)", "pipe_3(Red)"},
                                  {"pipe_2(Blue)", "pipe_1(Green)",   "pipe_2(Red)"},
                                  {"pipe_3(Blue)", "pipe_2(Green-2)", "pipe_1(Red)"}};

    private enum pipeState{
        None,
        Generating,
        Generated,
        Interrupted,
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
        if(isTest){
            for (int i = 0; i < 3; i++){
                for (int j = 0; j < 3; j++){
                    if(Input.GetKeyDown((KeyCode)(KeyCode.Keypad1 + (3*i + j)))){
                        StartCoroutine(CreatePipeGrad(i, j));
                        // CreatePipe(i, j);
                    }
                }
            }
        }
    }

    void CreatePipe(int from, int to){
        for (int i = 0; i < 4; i++){
            ChangeActiveState(pipeParts[from, to, i]);
        }
    }

    void ChangeActiveState(GameObject g){
        g.SetActive(!g.activeSelf);
    }

    void OnDataReceived(string message){
        int data = Int32.Parse(message);
        // Debug.Log(message);
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                bool isActive = ((data & (1 << (3*i + j))) > 0);
                if(isActive && pipeStates[i, j] == pipeState.None){
                    pipeStates[i, j] = pipeState.Generating;
                    StartCoroutine(CreatePipeGrad(i, j));
                }
                else if(!isActive &&pipeStates[i, j] == pipeState.Generated){
                    pipeStates[i, j] = pipeState.Destroying;
                    StartCoroutine(DestroyPipeGrad(i, j));
                }
                // for(int k = 0; k < 4; k++){
                //     pipeParts[i, j, k].SetActive(isActive);
                // }
            }
        }
    }

    IEnumerator CreatePipeGrad(int from, int to){
        for(int i = 0; i < 4; i++){
            pipeParts[from, to, i].SetActive(true);
            sePlayer.Play((int)SE_Manager.kind.Generate_pipe);
            if(i != 3) yield return new WaitForSeconds(PIPE_GENERATE_DURATION);
        }
        pipeStates[from, to] = pipeState.Generated;
    }

    IEnumerator DestroyPipeGrad(int from, int to){
        for(int i = 0; i < 4; i++){
            pipeParts[from, to, i].SetActive(false);
            sePlayer.Play((int)SE_Manager.kind.Destroy_pipe);
            if(i != 3) yield return new WaitForSeconds(PIPE_GENERATE_DURATION);
        }
        pipeStates[from, to] = pipeState.None;
    }


}
