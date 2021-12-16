using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -20){
            Destroy(gameObject); 
            Debug.Log("destroy ball");
        }
    }
}
