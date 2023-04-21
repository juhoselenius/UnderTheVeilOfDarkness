using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkeletonWalkSound : MonoBehaviour
{
    public List<AudioClip> WalkSounds;
    public AudioSource audioSource;
    public int pos;

    public GameObject soundText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        pos = (int)Mathf.Floor(Random.Range(0, WalkSounds.Count));
        audioSource.PlayOneShot(WalkSounds[pos]);

        Instantiate(soundText, gameObject.transform.position, Quaternion.identity);
    }
}
