using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f; // Thời gian mờ dần

    private void Start()
    {
        StartCoroutine(FadeIn()); // Khi vào scene, màn hình sáng dần
    }

    public void ChangeScene()
    {
        StartCoroutine(FadeOut()); // Khi chuyển scene, màn hình đen dần
    }

    private IEnumerator FadeIn()
    {
        fadeCanvasGroup.gameObject.SetActive(true); // Đảm bảo CanvasGroup được kích hoạt
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = 1 - (time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0; // Đảm bảo về 0
        yield return new WaitForSeconds(0.5f); // Đợi một chút trước khi bắt đầu fade out
        fadeCanvasGroup.gameObject.SetActive(false); // Tắt CanvasGroup sau khi fade in hoàn tất
    }

    private IEnumerator FadeOut()
    {
        fadeCanvasGroup.gameObject.SetActive(true);
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = time / fadeDuration;
            yield return null;
        }
        fadeCanvasGroup.alpha = 1; // Đảm bảo tối hoàn toàn
        yield return new WaitForSeconds(0.5f);
        fadeCanvasGroup.gameObject.SetActive(false);
    }
}
