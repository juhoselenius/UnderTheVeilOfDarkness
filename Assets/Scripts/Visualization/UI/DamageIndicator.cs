using System;
using System.Collections;
using UnityEngine;

/*
 * Code from the tutorial "Unity Tutorial - How to create a Damage Indicator"
 * by WatchFindDo Media on YouTube (https://youtu.be/BC3AKOQUx04)
*/

namespace Visualization
{
    public class DamageIndicator : MonoBehaviour
    {
        public float maxTimer;
        private float timer;
    
        public Transform target;
        private Transform player;

        private RectTransform rect;
        private CanvasGroup canvasGroup;

        private IEnumerator IE_Countdown;
        private Action unRegister;

        private Quaternion targetRotation;
        private Vector3 targetPosition;
    
        // Start is called before the first frame update
        void Awake()
        {
            targetRotation = Quaternion.identity;
            targetPosition = Vector3.zero;
            rect = GetComponent<RectTransform>();
            timer = maxTimer;
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Register(Transform target, Transform player, Action unRegister)
        {
            this.target = target;
            this.player = player;
            this.unRegister = unRegister;

            StartCoroutine(RotateToTarget());
            StartTimer();
        }

        public void Restart()
        {
            timer = maxTimer;
            StartTimer();
        }

        private void StartTimer()
        {
            if(IE_Countdown != null)
            {
                StopCoroutine(IE_Countdown);
            }

            IE_Countdown = Countdown();
            StartCoroutine(IE_Countdown);
        }

        IEnumerator RotateToTarget()
        {
            while(enabled)
            {
                if(target)
                {
                    targetPosition = target.position;
                    targetRotation = target.rotation;
                }

                Vector3 direction = player.position - targetPosition;

                targetRotation = Quaternion.LookRotation(direction);
                targetRotation.z = -targetRotation.y;
                targetRotation.x = 0;
                targetRotation.y = 0;

                Vector3 northDirection = new Vector3(0, 0, player.eulerAngles.y);
                rect.localRotation = targetRotation * Quaternion.Euler(northDirection);

                yield return null;
            }
        }

        private IEnumerator Countdown()
        {
            while(canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += 4 * Time.deltaTime;
                yield return null;
            }
        
            while(timer > 0)
            {
                timer--;
                yield return new WaitForSeconds(1);
            }

            while(canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 2 * Time.deltaTime;
                yield return null;
            }

            unRegister();
            Destroy(gameObject);
        }
    }

}
