using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera newCamera; // Camera sẽ đổi sang
    [SerializeField] private float transitionTime = 1.5f; // Thời gian blend
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private CanvasGroup fadeCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Khi nhân vật chạm vào
        {
            newCamera.Priority = 20; // Tăng Priority để Cinemachine chọn cam này
            StartCoroutine(ShowMessage(20f));
            
        }
    }

    private IEnumerator ShowMessage(float duration)
    {
        yield return new WaitForSeconds(5f);
        fadeCanvas.alpha = 0;
        sceneTransition.ChangeScene();
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageText.gameObject.SetActive(false);
    }
}
