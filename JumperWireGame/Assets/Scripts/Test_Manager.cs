using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Manager : MonoBehaviour
{
    [SerializeField] private bool isTest = false;
    [SerializeField] private Canvas debugCanvas;

    // Start is called before the first frame update
    void Start(){
        debugCanvas.gameObject.SetActive(isTest);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfTest(){
        return isTest;
    }


}
