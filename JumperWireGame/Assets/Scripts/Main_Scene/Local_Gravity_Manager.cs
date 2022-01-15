using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local_Gravity_Manager : MonoBehaviour
{
    private Vector3 localGravity;
    private Rigidbody rBody;
    private const float SECONDPIPE_X = 4.0f;
    private const float DECELERATE_X = 10.0f;
    private const float END_X = 12.0f;

    [SerializeField] private Vector3[] GRAVITY = new[] {new Vector3(0.0f, -7.0f, 0.0f),
                                                        new Vector3(10.0f, -30.0f, 0.0f),
                                                        new Vector3(5.0f, -5.0f, 0.0f),
                                                        new Vector3(0.0f, -5.0f, 0.0f),
    };
    [SerializeField] private Vector3[] VELOCITY = new[] {new Vector3(0.0f, 0.0f, 0.0f),
                                                         new Vector3(5.0f, -10.0f, 0.0f),
                                                         new Vector3(0.0f, 0.0f, 0.0f),
                                                         new Vector3(0.5f, 0.0f, 0.0f),
    };

    private enum BallState{
        onStage,
        inPipeFirst,
        inPipeSecond,
        End
    }

    [SerializeField] private BallState state;

    private bool isXDecelerated = false;

    // Use this for initialization
    private void Start() {
        rBody = this.GetComponent<Rigidbody>();
        rBody.useGravity = false; // 最初にrigidBodyの重力を無効化
        state = BallState.onStage;
        ChangeGravityAndVelocity(state);
    }

    private void Update(){
        float x = transform.position.x;

        switch(state){
            case BallState.inPipeFirst:
                if(x >= SECONDPIPE_X){
                    state = BallState.inPipeSecond;
                    ChangeGravityAndVelocity(state);
                }
                break;

            case BallState.inPipeSecond:
                if(x >= DECELERATE_X){
                    state = BallState.Decelerating;
                    ChangeGravityAndVelocity(state);
                }
                break;

        }

        // if(state == BallState.Decelerating && rBody.velocity.x <= 1.0f){
        //     state = BallState.End;
        //     ChangeGravityAndVelocity(state);
        // }
    }


    public void ChangeStateTo_InPipeFirst(){
        state = BallState.inPipeFirst;
        ChangeGravityAndVelocity(state);
        Debug.Log("state is changed to inPipeFirst");
    }


    public void ChangeStateTo_End(){
        state = BallState.End;
        ChangeGravityAndVelocity(state);
        Debug.Log("state is changed to End");
    }


    void ChangeGravityAndVelocity(BallState state){
        localGravity = GRAVITY[(int)state];
        if(state == BallState.inPipeFirst || state == BallState.End){
            rBody.velocity = VELOCITY[(int)state];
        }
    }


    private void FixedUpdate() {
        SetLocalGravity(); // 重力をAddForceでかけるメソッドを呼ぶ。FixedUpdateが好ましい。
    }

    private void SetLocalGravity(){
        rBody.AddForce (localGravity, ForceMode.Acceleration);
    }

    private void ChangeLocalGravity_oneDimention(int dim, float gravity){
        switch(dim){
            case 0:
                localGravity.x = gravity;
                break;

            case 1:
                localGravity.y = gravity;
                break;

            case 2:
                localGravity.z = gravity;
                break;
        }
    }
}