using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Manager : MonoBehaviour
{
    // protected GameObject SE_Manager;
    protected Score_Manager scoreManager;
    protected SE_Manager sePlayer;
    private const float LIMIT_HEIGHT = -25.0f;

    // Start is called before the first frame update
    protected virtual void Start(){
        scoreManager = GameObject.Find("Score Manager").GetComponent<Score_Manager>();
        sePlayer = GameObject.Find("SE Manager").GetComponent<SE_Manager>();
    }

    // Update is called once per frame
    protected virtual void Update(){
        // ボールが落ちたら消す
        if(transform.position.y < LIMIT_HEIGHT){
            sePlayer.Play(SE_Manager.kind.Fall);
            Destroy(gameObject); 
            Debug.Log("destroy ball");
            scoreManager.AddDestroyedBall();
        }

        // ボールの大きさを変更する
        float heightFromBottom = transform.position.y - LIMIT_HEIGHT;
        float scale = Math.Abs((heightFromBottom / LIMIT_HEIGHT)) * 0.7f + 0.3f ;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void OnCollisionEnter(Collision collision){
        string colName = collision.gameObject.name;
        // Debug.Log(colName);
        if(colName.Split('_')[0] == "pipe" && colName[colName.Length -1] == '1'){
            // Debug.Log("PIPE IN FIRST");
            var localGravityManager = gameObject.GetComponent<Local_Gravity_Manager>();
            localGravityManager.ChangeStateTo_InPipeFirst();
        }
    }

    void OnTriggerEnter(Collider collider){
        string colName = collider.gameObject.name;
        // Debug.Log(colName);

        // バケツに入ったとき
        if(colName.Split(' ')[1] == "BucketTrigger"){
            Debug.Log("Bucket in");
            string bucket_color = collider.gameObject.name.Split(' ')[0];
            string ball_color = gameObject.GetComponent<Renderer>().material.name.Split(' ')[0];

            if(ball_color == bucket_color) BucketIsMatch();
            else BucketIsNotMatch();
            
            scoreManager.AddDestroyedBall();
            Destroy(gameObject);
        }
        // else Debug.Log("This is not a bucketTrigger");
    }

    protected virtual void BucketIsMatch(){
        Debug.Log("SAME COLOR");
        PlayOKSound();
        scoreManager.AddScore();
    }

    void BucketIsNotMatch(){
        Debug.Log("DIFFENRENT COLOR");
        PlayNGSound();
    }

    protected virtual void PlayOKSound(){
        sePlayer.Play(SE_Manager.kind.OK_ball);
    }

    protected virtual void PlayNGSound(){
        sePlayer.Play(SE_Manager.kind.NG);
    }
}
