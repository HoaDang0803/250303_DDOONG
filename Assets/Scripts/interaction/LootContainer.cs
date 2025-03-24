using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootContainer : Interactable
{
    [SerializeField] private GameObject opened;
    [SerializeField] private GameObject closed;
    [SerializeField] private bool isOpened;
    public override void Interact(Character character)
    {
        if (isOpened == false)
        {
            isOpened = true;
            closed.SetActive(false);
            opened.SetActive(true);
        }
        else
        {
            isOpened = false;
            closed.SetActive(true);
            opened.SetActive(false);
        }
    }
}
