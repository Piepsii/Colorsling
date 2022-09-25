using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audio : MonoBehaviour
{
    public string deviceName;
    public bool loop;
    public int length;

    private AudioSource audioSource;

    void Start()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(deviceName, loop, length, 44100);
        audioSource.loop = true;

        while (!(Microphone.GetPosition(null) > 0))
        {
            audioSource.Play();
        }
    }
}
