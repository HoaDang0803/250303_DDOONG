using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Image icon; 

    public void SetIcon(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.itemSprite;
    }

    public void ClearIcon()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }
}
