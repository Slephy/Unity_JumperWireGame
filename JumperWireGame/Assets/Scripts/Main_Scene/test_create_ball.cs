using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_create_ball : MonoBehaviour
{
    public GameObject ball_prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("space key pressed");
            GameObject ball = Instantiate(ball_prefab) as GameObject;
            // ball.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            ball.transform.position = new Vector3(-16.5f, 2.7f, Random.Range(-14.0f, -2.0f));
        }
    }

    
}
