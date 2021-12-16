using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule_Manager : Ball_Manager
{
    // Start is called before the first frame update
    protected override void Start(){
        base.Start();
    }

    // Update is called once per frame
    protected override void Update(){
        base.Update();
    }

    protected override void BucketIsMatch(){
        Debug.Log("SAME COLOR CAPSULE");
        se_manager.GetComponent<SE_Manager>().Play(1);
    }

    protected override void BucketIsNotMatch(){
        Debug.Log("DIFFERENT COLOR CAPSULE");
        se_manager.GetComponent<SE_Manager>().Play(2);
    }
}
