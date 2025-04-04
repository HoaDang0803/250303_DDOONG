using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    [SerializeField] private ItemContainer itemContainer;
    [SerializeField] private GameObject riverStateDefault;
    [SerializeField] private GameObject riverStateNormal;

    [SerializeField] private GameObject allItems;

    public GameObject messagePanel;
    public TextMeshProUGUI mesageText;
    private bool isPlayerNear = false;
    private void Update()
    {
        if (isPlayerNear && messagePanel.activeSelf == false)
        {
            OpenMessagePanel("Nhấn E để hoàn thành nghi lễ");
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(HandleRitualSequence());
        }
    }

    private IEnumerator HandleRitualSequence()
    {
        // Ẩn thông báo "Nhấn E để hoàn thành nghi lễ"
        messagePanel.SetActive(false);

        bool hasOil = IsItemActive("Oil");
        bool hasWood = IsItemActive("Wood");
        bool hasStone = IsItemActive("FireStone");

        if (!hasOil || !hasWood || !hasStone)
        {
            OpenMessagePanel("Bạn cần 3 vật phẩm: Oil, Wood, FireStone");
            yield return new WaitForSeconds(2f); // Giữ thông báo trong 2 giây
            messagePanel.SetActive(false);
            yield break;
        }

        // Nếu có đủ vật phẩm, thực hiện nghi lễ
        OpenMessagePanel("Nghi lễ đã hoàn thành! Đang kích hoạt...");
        yield return new WaitForSeconds(2f); // Chờ 2 giây trước khi thay đổi trạng thái sông

        allItems.SetActive(true);
        itemContainer.slots.Find(x => x.item != null && x.item.itemName == "Oil").item = null;
        itemContainer.slots.Find(x => x.item != null && x.item.itemName == "Wood").item = null;
        itemContainer.slots.Find(x => x.item != null && x.item.itemName == "FireStone").item = null;

        riverStateDefault.SetActive(false);
        riverStateNormal.SetActive(true);

        // Hiển thị thông báo hoàn tất
        OpenMessagePanel("Nghi lễ đã hoàn tất! Dòng sông đã thay đổi.");
        yield return new WaitForSeconds(2f);
        messagePanel.SetActive(false);
    }

    private bool IsItemActive(string itemName)
    {
        return itemContainer.slots.Exists(slot => slot.item != null && slot.item.itemName == itemName);
    }

    // Khi người chơi vào vùng sông
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Khi người chơi rời khỏi vùng sông
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    public void OpenMessagePanel(string message)
    {
        mesageText.text = message;
        Debug.Log("Open message panel");
        messagePanel.SetActive(true);
    }
}
