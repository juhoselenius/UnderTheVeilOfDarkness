using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Logic.Player;

public class SkeletonWalkSound : MonoBehaviour
{
    private IPlayerManager _playerManager;

    public List<AudioClip> WalkSounds;
    public AudioSource audioSource;
    public int pos;

    public GameObject soundText;

    void Awake()
    {
        _playerManager = ServiceLocator.GetService<IPlayerManager>();
    }

    public void playSound()
    {
        pos = (int)Mathf.Floor(Random.Range(0, WalkSounds.Count));
        audioSource.PlayOneShot(WalkSounds[pos]);

        if(_playerManager.GetHearing() == 2f)
        {
            soundText.GetComponent<SoundToText>().textSound = "Stomp";
            Instantiate(soundText, gameObject.transform.position, Quaternion.identity);
        }
    }
}
