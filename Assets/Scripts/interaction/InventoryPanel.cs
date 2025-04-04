using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private ItemContainer inventory;
    [SerializeField] private List<InventoryButton> buttons;
    private void Start()
    {
        Show();
    }

    public void Update()
    {
        Show();
    }


    private void Show()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item != null)
            {
                buttons[i].SetIcon(inventory.slots[i]);
            }
            else
            {
                buttons[i].ClearIcon();
            }
        }
    }
}
