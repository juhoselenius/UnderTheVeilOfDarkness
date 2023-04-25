using TMPro;
using UnityEngine;

namespace Visualization
{
    public class SoundToText : MonoBehaviour
    {
        public string textSound;
        public float moveSpeed;
        public float textLifeTime;

        public TextMeshPro soundText;

        private Transform playerTransform;
        private float remainingLifetime;

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

            remainingLifetime = textLifeTime;
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

            // Calculate the new scale based on remaining lifetime.
            float scale = remainingLifetime / textLifeTime;
            soundText.transform.localScale = new Vector3(scale, scale, scale);

            remainingLifetime -= Time.deltaTime;

            if (remainingLifetime <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
