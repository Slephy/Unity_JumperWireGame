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
    }

    void OnCollisionEnter(Collision collision){
         // バケツに入ったとき
        if(collision.gameObject.name.Split(' ')[0] == "Bucket"){
            Debug.Log("Bucket in");
            string bucket_color = collision.gameObject.name.Split(' ')[1];
            string ball_color = gameObject.GetComponent<Renderer>().material.name.Split(' ')[0];

            if(ball_color == bucket_color) BucketIsMatch();
            else BucketIsNotMatch();
            
            scoreManager.AddDestroyedBall();
            Destroy(gameObject);
        }
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
