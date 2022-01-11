using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule_Manager : Ball_Manager
{
    protected Gacha_Rotater gachaRotater;

    // Start is called before the first frame update
    protected override void Start(){
        base.Start();
        gachaRotater = GameObject.Find("Gacha Rotater").GetComponent<Gacha_Rotater>();
    }

    // Update is called once per frame
    protected override void Update(){
        base.Update();
    }

    protected override void BucketIsMatch(){
        base.BucketIsMatch();
        // ＊シリアル通信でカプセルを回す指示＊
        gachaRotater.RotateGacha();
    }

    protected override void PlayOKSound(){
        sePlayer.Play(SE_Manager.kind.OK_capsule);
    }
}
