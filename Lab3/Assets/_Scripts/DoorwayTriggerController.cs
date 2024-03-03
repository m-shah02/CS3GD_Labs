using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayTriggerController : MonoBehaviour
{
    private AudioSource noise;

    private void Awake()
    {
        noise = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            noise.Play();
        }
    }
}
