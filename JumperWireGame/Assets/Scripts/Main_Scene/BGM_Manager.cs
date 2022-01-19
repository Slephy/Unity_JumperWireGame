using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    public void PlayBGM(){
        audioSource.Play();
    }

    public void StopBGM(){
        audioSource.Stop();
    }
}
