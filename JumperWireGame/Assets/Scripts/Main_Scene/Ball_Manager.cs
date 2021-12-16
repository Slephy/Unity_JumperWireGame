using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Manager : MonoBehaviour
{
    // private var rgx = new Regex(@"Bucket\s[a-zA-Z]*");
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    protected virtual void BucketIsMatch(){
        Debug.Log("SAME COLOR");
        
    }

    protected virtual void BucketIsNotMatch(){
        Debug.Log("DIFFENRENT COLOR");

    }
}
