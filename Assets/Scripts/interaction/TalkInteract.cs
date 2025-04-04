using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : Interactable
{
    public GameObject messagePanel;
    public TMPro.TextMeshProUGUI messageText;

    public GameObject fireStone;
    private bool isInteracting = false; // Biến để theo dõi trạng thái tương tác

    public override void Interact(Character character)
    {
        if (!isInteracting) // Chỉ cho phép nhấn E một lần để tránh spam text
        {
            StartCoroutine(ShowDialogue());
        }
    }

    public override void Uninteract(Character character)
    {
        CloseMessagePanel();
    }

    private IEnumerator ShowDialogue()
    {
        isInteracting = true; // Đánh dấu người chơi đang tương tác

        OpenMessagePanel("Nhấn E để nói chuyện");

        // Chờ người chơi nhấn E
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        OpenMessagePanel("Đây là đá lửa được tạo ra từ dung nham. Nó có thể sẽ giúp con trên hành trình này.");

        // Đợi 3 giây trước khi đóng thông báo
        yield return new WaitForSeconds(3f);

        // Đóng thông báo
        CloseMessagePanel();
        fireStone.SetActive(true);
    }
    public void OpenMessagePanel(string message)
    {
        messagePanel.SetActive(true);
        messageText.text = message;
    }

    public void CloseMessagePanel()
    {
        messagePanel.SetActive(false);
    }
}

