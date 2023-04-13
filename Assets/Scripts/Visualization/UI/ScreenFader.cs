using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class ScreenFader : MonoBehaviour
    {
        public bool fadeOnStart = true;
        public float fadeDuration = 2f;
        public Color fadeColor;
        public Image image;

        private void Start()
        {
            if (fadeOnStart)
            {
                FadeIn();
            }
        }

        public void FadeIn()
        {
            Fade(1, 0);
        }

        public void FadeOut()
        {
            Fade(0, 1);
        }

        public void Fade(float alphaIn, float alphaOut)
        {
            StartCoroutine(FadeRoutine(alphaIn, alphaOut));
        }

        public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
        {
            float timer = 0;
            while (timer < fadeDuration)
            {
                Color newColor = fadeColor;
                newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

                image.color = newColor;

                timer += Time.deltaTime;
                yield return null;
            }

            Color newColor2 = fadeColor;
            newColor2.a = alphaOut;
            image.color = newColor2;
        }
    }
}
