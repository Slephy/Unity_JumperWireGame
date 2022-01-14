using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Manager : MonoBehaviour
{
    [SerializeField] private static bool isTest = false;
    [SerializeField] private Canvas debugCanvas = null;

    // Start is called before the first frame update
    void Start(){
        if(debugCanvas != null) debugCanvas.gameObject.SetActive(isTest);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfTest(){
        return isTest;
    }

    public void ChangeIsTest(bool isTest){
        Test_Manager.isTest = isTest;
    }


}
