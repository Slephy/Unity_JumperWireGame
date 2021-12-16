using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_create_ball : MonoBehaviour
{
    public GameObject ball_prefab;
    public Material blue;
    public Material green;
    public Material red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            CreateBall("red");
        }
    }

    private void CreateBall(string color, bool isCupsule = false){
        Debug.Log("space key pressed");
        GameObject ball = Instantiate(ball_prefab) as GameObject;
        ball.transform.position = new Vector3(-16.5f, 2.7f, Random.Range(-14.0f, -2.0f));
        switch (color)
        {
            case "blue":
                ball.GetComponent<Renderer>().material = blue;
                break;
            case "green":
                ball.GetComponent<Renderer>().material = green;
                break;
            case "red":
                ball.GetComponent<Renderer>().material = red;
                break;
        }
    }

    
}
