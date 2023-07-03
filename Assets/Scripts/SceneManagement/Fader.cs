using System;
using System.Collections;
using UnityEngine;

namespace Game.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup = null;


        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            

        }

        //private IEnumerator FadeOutIn()
        //{
        //    yield return FadeOut(fadeOutTime);

        //    yield return FadeIn(fadeInTime);
        //}

        public IEnumerator FadeOut(float time)
        {

            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }

        }
        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}
