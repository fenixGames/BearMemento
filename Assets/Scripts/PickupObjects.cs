using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    public Inventory inventory;
    public string dialogText = "";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inventory.AddObject(gameObject);
            gameObject.SetActive(false);

            DialogueSystem.Instance.SetDialogue(dialogText);
        }
    }
}
