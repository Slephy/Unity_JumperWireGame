using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_create_ball : MonoBehaviour
{
    public GameObject ball_prefab;
    public GameObject capsule_prefab;
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
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     CreateBallOrCapsule("red");
        // }
        if(Input.GetKeyDown(KeyCode.Alpha1)) CreateSomething("blue");
        if(Input.GetKeyDown(KeyCode.Alpha2)) CreateSomething("red");
        if(Input.GetKeyDown(KeyCode.Alpha3)) CreateSomething("green");
    }

    private void CreateSomething(string color){
        if(Input.GetKey(KeyCode.C)) CreateBallOrCapsule(color, true);
        else CreateBallOrCapsule(color);
    }

    private void CreateBallOrCapsule(string color, bool isCapsule = false){
        Debug.Log("space key pressed");
        GameObject inst;
        if(isCapsule) inst = Instantiate(capsule_prefab) as GameObject;
        else inst = Instantiate(ball_prefab) as GameObject;
        // GameObject inst = Instantiate(ball_prefab) as GameObject;
        inst.transform.position = new Vector3(-16.5f, 2.7f, Random.Range(-14.0f, -2.0f));
        switch (color)
        {
            case "blue":
                inst.GetComponent<Renderer>().material = blue;
                break;
            case "green":
                inst.GetComponent<Renderer>().material = green;
                break;
            case "red":
                inst.GetComponent<Renderer>().material = red;
                break;
        }
    }

    
}