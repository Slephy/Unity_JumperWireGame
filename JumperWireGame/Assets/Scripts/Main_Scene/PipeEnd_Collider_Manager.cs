using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeEnd_Collider_Manager : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider + "is entered");
        var lGrav = collider.gameObject.GetComponent<Local_Gravity_Manager>();
        lGrav.ChangeStateTo_End();
    }
}
