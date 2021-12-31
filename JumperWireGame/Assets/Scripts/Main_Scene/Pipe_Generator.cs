using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Generator : MonoBehaviour
{
    private GameObject[,,] pipeParts = new GameObject[3, 3, 4];
    private string[,] pipeName = {{"pipe_1(Blue)", "pipe_2(Green-1)", "pipe_3(Red)"},
                                  {"pipe_2(Blue)", "pipe_1(Green)",   "pipe_2(Red)"},
                                  {"pipe_3(Blue)", "pipe_2(Green-2)", "pipe_1(Red)"}};
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 3; j++){
                Transform pipe = GameObject.Find(pipeName[i, j]).GetComponent<Transform>();
                for (int k = 0; k < 4; k++){
                    char pipe_group = pipeName[i, j][5];
                    string child_index = (k+1).ToString();
                    string childName = "pipe_" + pipe_group + "-" + child_index;
                    Debug.Log(childName);
                    pipeParts[i, j, k] = pipe.Find(childName).gameObject;
                    pipeParts[i, j, k].SetActive(false);
                    // if(pipeParts[i, j, k] == null) Debug.LogFormat("{1} {2} {3} is null", i, j, k);
                    // else Debug.LogFormat("{1} {2} {3} is created", i, j, k);
                }
            }
        }

    }

    // Update is called once per frame
    void Update(){
        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 3; j++){
                if(Input.GetKeyDown((KeyCode)(KeyCode.Keypad1 + (3*i + j)))){
                    CreatePipe(i, j);
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
}
