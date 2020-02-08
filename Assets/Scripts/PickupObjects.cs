using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    public Inventory inventory;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inventory.AddObject(gameObject);
            gameObject.SetActive(false);
        }
    }
}
