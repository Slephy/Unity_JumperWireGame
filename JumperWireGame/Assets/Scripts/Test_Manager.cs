using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Manager : MonoBehaviour
{
    [SerializeField] private bool isTest = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfTest(){
        return isTest;
    }
}