using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesCut : ToolHit
{
    [SerializeField] private GameObject pickUpDrop;
    [SerializeField] private int dropCount = 5;
    [SerializeField] private float spread = 0.5f;

    
    public override void Hit()
    { 
        while (dropCount > 0)
        {
            dropCount--;
            Vector3 position = transform.position;
            position.x += spread*Random.value - spread/2;
            position.y += spread*Random.value - spread/2;
            GameObject go = Instantiate(pickUpDrop);
            go.transform.position = position;
        }

        Destroy(gameObject);
    }
}
