using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer audioMe;

    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioMe = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayOneShot(AudioClip audioClip)
    {
        audioMe.audioSource.PlayOneShot(audioClip);
    }
}
