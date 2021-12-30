using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local_Gravity_Manager : MonoBehaviour
{
    private Vector3 localGravity = new Vector3(0.0f, -5.0f, 0.0f);
    private Rigidbody rBody;
    private const float BEGIN_X = -2.9f;
    private const float DECELERATE_X = 13.0f;
    private const float END_X = 15.0f;
    
    private enum BallState{
        onStage,
        inPipeFirst,
        inPipeSecond,
        Decelerating,
        End
    }

    private BallState state = BallState.onStage;

    // Use this for initialization
    private void Start() {
        rBody = this.GetComponent<Rigidbody>();
        rBody.useGravity = false; // 最初にrigidBodyの重力を使わなくする
    }

    private void Update(){
        float x = transform.position.x;

        switch(state){
            case BallState.onStage:
                if(x >= BEGIN_X){
                    state = BallState.inPipeFirst;
                    rBody.velocity = new Vector3(5.0f, 0.0f, 0.0f);
                    localGravity = new Vector3(10.0f, -15.0f, 0.0f);
                }
                break;

            case BallState.inPipeFirst:
                if(x >= DECELERATE_X){
                    state = BallState.inPipeSecond;
                    localGravity = new Vector3(10.0f, -5.0f, 0.0f);
                }
                break;

            case BallState.inPipeSecond:
                if(x >= DECELERATE_X){
                    state = BallState.Decelerating;
                    localGravity = new Vector3(0.0f, -5.0f, 0.0f);
                }
                break;

            case BallState.Decelerating:
                if(x >= END_X){
                    state = BallState.End;
                    rBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    localGravity = new Vector3(0.0f, -5.0f, 0.0f);
                }
                break;
        }
    }

    private void FixedUpdate() {
        SetLocalGravity(); // 重力をAddForceでかけるメソッドを呼ぶ。FixedUpdateが好ましい。
    }

    private void SetLocalGravity(){
        rBody.AddForce (localGravity, ForceMode.Acceleration);
    }
}