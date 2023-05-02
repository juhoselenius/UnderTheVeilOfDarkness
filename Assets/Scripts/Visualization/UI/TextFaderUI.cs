using System.Collections;
using TMPro;
using UnityEngine;

namespace Visualization
{
    public class TextFaderUI : MonoBehaviour
    {
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI contentText;

        public float waitTime;
        public float fadeOutTime;
        
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(FadeOut());
        }

        IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(waitTime);
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / fadeOutTime;
                Color titleColor = titleText.color;
                titleColor.a = Mathf.Lerp(1, 0, t);
                titleText.color = titleColor;

                Color contentColor = contentText.color;
                contentColor.a = Mathf.Lerp(1, 0, t);
                contentText.color = contentColor;

                yield return null;
            }
            Destroy(gameObject);
        }
    }

}
