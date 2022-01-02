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
        Generate_balls,
        Generate_pipe,
        Destroy_pipe,
        Fall,
        CountDown,
        GameStart,
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

    public void Play(int SENumber){
        audioSource.PlayOneShot(se[SENumber]);
    }
}
