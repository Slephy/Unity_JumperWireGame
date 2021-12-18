using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Manager : MonoBehaviour
{
    protected GameObject se_manager;
    protected GameObject Score_Manager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        se_manager = GameObject.Find("SE Manager");
        Score_Manager = GameObject.Find("Score Manager");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // ボールが落ちたら消す
        if(transform.position.y < -20){
            Destroy(gameObject); 
            Debug.Log("destroy ball");
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name.Split(' ')[0] == "Bucket"){ // バケツに入ったときの処理
            // Debug.Log("backet in");
            string bucket_color = collision.gameObject.name.Split(' ')[1];
            // Debug.Log("bucket color is " + bucket_color);
            string ball_color = gameObject.GetComponent<Renderer>().material.name.Split(' ')[0];
            // Debug.Log("ball color is " + ball_color);

            if(ball_color == bucket_color) BucketIsMatch();
            else BucketIsNotMatch();
            
            Destroy(gameObject);
        }
    }

    void BucketIsMatch(){
        Debug.Log("SAME COLOR");
        PlayOKSound();
        Score_Manager.GetComponent<Score_Manager>().AddScore();
    }

    protected virtual void BucketIsNotMatch(){
        Debug.Log("DIFFENRENT COLOR");
        PlayNGSound();
    }

    protected virtual void PlayOKSound(){
        se_manager.GetComponent<SE_Manager>().Play(0);
    }

    protected virtual void PlayNGSound(){
        se_manager.GetComponent<SE_Manager>().Play(2);
    }
}
