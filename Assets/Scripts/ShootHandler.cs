using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    public AudioSource arrowAudio;

    public AudioSource chopAudio;

    

    public void ShootEvent()
    {
        arrowAudio.Play();
    }

    public void ChopEvent()
    {
        chopAudio.Play();
    }
}
