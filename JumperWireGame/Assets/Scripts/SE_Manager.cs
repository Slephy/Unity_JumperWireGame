using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Manager : MonoBehaviour
{
    [SerializeField] private AudioClip[] se;
    AudioSource audioSource;

    public enum kind{
        OK_ball,
        OK_capsule,
        NG,
        Generate_ball,
        Generate_capsule,
        Generate_pipe,
        Destroy_pipe,
        Fall,
        Count_Down,
        Count_GameStart,
        result_don,
        // result_dodon,
        result_perfect,
        result_great,
        result_good,
        result_bad,
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(kind seKind){
        audioSource.PlayOneShot(se[(int)seKind]);
    }
}
