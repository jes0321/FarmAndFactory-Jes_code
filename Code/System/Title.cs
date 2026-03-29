using System.Collections;
using DG.Tweening;
using TransitionsPlus;
using UnityEngine;

namespace JES.Code.System
{
    public class Title : MonoBehaviour
    {
        [SerializeField] private TransitionAnimator[] _animator;
        public void QuitGame()
        {
            Application.Quit();
        }
        public async void LoadGame()
        {
            StartCoroutine(PlayTransitionLerp(_animator[0], 2.0f));
            await Awaitable.WaitForSecondsAsync(0.5f);
            StartCoroutine(PlayTransitionLerp(_animator[1], 2.0f));
            await Awaitable.WaitForSecondsAsync(1.0f);
            StartCoroutine(PlayTransitionLerp(_animator[2], 1.5f));
            await Awaitable.WaitForSecondsAsync(1.3f);
            StopAllCoroutines();
            UnityEngine.SceneManagement.SceneManager.LoadScene("JesScene");
        }
        public IEnumerator PlayTransitionLerp(TransitionAnimator animator, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                float t = Mathf.Clamp01(elapsed / duration);
                animator.SetProgress(t);
                elapsed += Time.deltaTime;
                yield return null;
            }
            animator.SetProgress(1f); // 마지막에 1로 보정
        }
    } 
}