using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundToText : MonoBehaviour
{
    public string textSound;
    public float moveSpeed;
    public float textLifeTime;

    public TextMeshPro soundText;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        soundText.text = textSound;
        transform.LookAt(playerTransform);
        
        // Rotate additional 30 degrees
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.z -= 30f;
        transform.localEulerAngles = newRotation;

        Destroy(gameObject, textLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the amount to move the object this frame
        float distanceToMove = -moveSpeed * Time.deltaTime;

        // Calculate the direction to move in based on the current rotation of the object
        Vector3 moveDirection = transform.rotation * Vector3.right;

        // Move the object in the calculated direction
        transform.Translate(moveDirection * distanceToMove, Space.World);
    }
}
