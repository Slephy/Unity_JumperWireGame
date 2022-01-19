using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local_Gravity_Manager : MonoBehaviour
{
    private Vector3 localGravity;
    private Rigidbody rBody;
    private const float SECONDPIPE_X = 4.0f;
    // private const float DECELERATE_X = 10.0f;
    // private const float END_X = 12.0f;

    private Vector3[] GRAVITY = new[] {new Vector3(0.0f, -7.0f, 0.0f),
                                       new Vector3(10.0f, -10.0f, 0.0f),
                                       new Vector3(5.0f, -5.0f, 0.0f),
                                       new Vector3(0.0f, -5.0f, 0.0f),
    };

    enum BallState{
        onStage,
        inPipeFirst,
        inPipeSecond,
        End
    }

    [SerializeField] private BallState state;


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
        }
    }


    void ChangeGravityAndVelocity(BallState state){
        localGravity = GRAVITY[(int)state];
    }


    private void FixedUpdate() {
        SetLocalGravity(); // 重力をAddForceでかけるメソッドを呼ぶ。FixedUpdateが好ましい。
    }

    private void SetLocalGravity(){
        rBody.AddForce (localGravity, ForceMode.Acceleration);
    }


    public void ChangeStateTo_InPipeFirst(){
        state = BallState.inPipeFirst;
        ChangeGravityAndVelocity(state);
        // StartCoroutine(pushBall());
        Debug.Log("state is changed to inPipeFirst");
    }


    public void ChangeStateTo_End(){
        state = BallState.End;
        bind_XZpos();
        ChangeGravityAndVelocity(state);
        Debug.Log("state is changed to End");
    }


    void bind_XZpos(){
        rBody.constraints = RigidbodyConstraints.FreezePositionX;
        rBody.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    IEnumerator pushBall(){
        rBody.AddForce(new Vector3(7, 0, 0), ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        rBody.AddForce(new Vector3(0, -7, 0), ForceMode.Impulse);
    }
}