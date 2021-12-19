using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Manager : MonoBehaviour
{
    [SerializeField] private AudioClip[] se;
    AudioSource audioSource;

    public enum SE_kind{
        OK_ball,
        OK_capsule,
        NG,
        Set_Pipe,
        Generate
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
