using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha_Rotater : MonoBehaviour
{
    Serial_Handler serialHandler;
    [SerializeField] Serial_Initializer serialInitializer;
    // Start is called before the first frame update
    void Start(){
        serialHandler = serialInitializer.InitializeSerialHandler(false);
    }


    public void RotateGacha(){
        serialHandler.Write("1");
    }
}
