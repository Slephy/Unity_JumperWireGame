using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule_Manager : Ball_Manager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void BucketIsMatch(){
        Debug.Log("SAME COLOR CAPSULE");
    }

    protected override void BucketIsNotMatch(){
        Debug.Log("DIFFERENT COLOR CAPSULE");
    }
}
