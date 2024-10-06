using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (musicSource.isPlaying)
            {

                musicSource.Stop();
            }
            else musicSource.Play();
        }
    }
}
