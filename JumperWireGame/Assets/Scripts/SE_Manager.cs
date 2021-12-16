using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Manager : MonoBehaviour
{
    [SerializeField] private AudioClip[] se;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Q)) Play();
    }

    public void Play(int SENumber){
        audioSource.PlayOneShot(se[SENumber]);
    }
}
