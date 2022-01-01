using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Manager : MonoBehaviour
{
    // protected GameObject SE_Manager;
    protected Score_Manager scoreManager;
    protected SE_Manager sePlayer;

    // Start is called before the first frame update
    protected virtual void Start(){
        scoreManager = GameObject.Find("Score Manager").GetComponent<Score_Manager>();
        sePlayer = GameObject.Find("SE Manager").GetComponent<SE_Manager>();
    }

    // Update is called once per frame
    protected virtual void Update(){
        // ボールが落ちたら消す
        if(transform.position.y < -25){
            Destroy(gameObject); 
            Debug.Log("destroy ball");
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name.Split(' ')[0] == "Bucket"){ // バケツに入ったときの処理
            Debug.Log("Bucket in");
            string bucket_color = collision.gameObject.name.Split(' ')[1];
            string ball_color = gameObject.GetComponent<Renderer>().material.name.Split(' ')[0];

            if(ball_color == bucket_color) BucketIsMatch();
            else BucketIsNotMatch();
            
            Destroy(gameObject);
        }
    }

    void BucketIsMatch(){
        Debug.Log("SAME COLOR");
        PlayOKSound();
        scoreManager.AddScore();
    }

    protected virtual void BucketIsNotMatch(){
        Debug.Log("DIFFENRENT COLOR");
        PlayNGSound();
    }

    protected virtual void PlayOKSound(){
        sePlayer.Play((int)SE_Manager.kind.OK_ball);
    }

    protected virtual void PlayNGSound(){
        sePlayer.Play((int)SE_Manager.kind.NG);
    }
}
