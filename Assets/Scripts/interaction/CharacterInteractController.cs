using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    PlayerController playerControler;
    Rigidbody rb;
    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfHitBox = 1.2f;
    Character character;
    [SerializeReference] HighlightController highlightController;

    private void Awake()
    {
        playerControler = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        Check();

        Collider[] colliders = Physics.OverlapSphere(transform.position, sizeOfHitBox);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("NPC"))
            {
                Vector3 posA = new Vector3(collider.transform.position.x, 0, collider.transform.position.z);
                Vector3 posB = new Vector3(transform.position.x, 0, transform.position.z);
                if (Vector3.Distance(posA, posB) < offsetDistance)
                {
                    collider.GetComponent<Interactable>().Interact(character);
                }
                else
                {
                    collider.GetComponent<Interactable>().Uninteract(character);
                }
            }
        }
    }

    private void Check()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sizeOfHitBox);

        foreach (var collider in colliders)
        {
            Vector3 posA = new Vector3(collider.transform.position.x, 0, collider.transform.position.z);
            Vector3 posB = new Vector3(transform.position.x, 0, transform.position.z);

            if (collider.gameObject.CompareTag("NPC"))
            {
                if (Vector3.Distance(posA, posB) < offsetDistance)
                {

                    highlightController.Highlight(collider.gameObject);
                    return;
                }
            }
        }

        highlightController.Hide();
    }

}
