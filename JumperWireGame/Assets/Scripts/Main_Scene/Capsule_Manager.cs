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
        base.BucketIsMatch();
        // ＊シリアル通信でカプセルを回す指示＊
    }

    protected override void PlayOKSound(){
        sePlayer.Play((int)SE_Manager.kind.OK_capsule);
    }
}
