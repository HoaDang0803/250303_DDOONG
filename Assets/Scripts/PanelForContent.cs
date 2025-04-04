using System.Collections;
using UnityEngine;

public class PanelForContent : MonoBehaviour
{
    public GameObject canvas;
    public CanvasGroup panel1; // Panel đầu tiên
    public CanvasGroup panel2; // Panel thứ hai
    public float fadeDuration = 1f; // Thời gian hiệu ứng fade

    private int clickCount = 0; // Đếm số lần click
    private bool isFading = false; // Tránh spam input

    void Start()
    {
        //PlayerPrefs.DeleteKey("CanvasSeen");
        // Nếu người chơi đã xem qua, tắt canvas ngay lập tức
        if (PlayerPrefs.GetInt("CanvasSeen", 0) == 1)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
        }
    }

    void Update()
    {
        if (canvas.activeSelf == false) return;
        if (Input.anyKeyDown && !isFading) // Chạy fade chỉ khi chưa hoàn thành
        {
            clickCount++;
            if (clickCount == 1)
            {
                StartCoroutine(FadeOut(panel1));
            }
            else if (clickCount == 2)
            {
                StartCoroutine(FadeOut(panel2));
                StartCoroutine(FinishFade());
            } // Ẩn panel2 khi nhấn lần thứ hai
        }
    }

    IEnumerator FadeOut(CanvasGroup panel)
    {
        isFading = true;
        float startAlpha = panel.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            yield return null;
        }

        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
        isFading = false;
    }

    IEnumerator FinishFade()
    {
        yield return new WaitForSeconds(fadeDuration); // Đợi thời gian fade xong

        // Lưu trạng thái đã xem qua panel
        PlayerPrefs.SetInt("CanvasSeen", 1);
        PlayerPrefs.Save(); // Đảm bảo dữ liệu được lưu lại

        canvas.SetActive(false);
    }
}
