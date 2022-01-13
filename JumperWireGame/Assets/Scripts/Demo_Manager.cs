using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Manager : MonoBehaviour
{
    public static bool isDemo = false;

    public void ChangeIsDemo(bool isDemo){
        Demo_Manager.isDemo = isDemo;
    }
}
