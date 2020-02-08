using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    public Inventory inventory;
    public string dialogText = "";
    public GameObject dialogUI;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inventory.AddObject(gameObject);
            gameObject.SetActive(false);

            TextMeshPro dialogTextMesh = dialogUI.GetComponent<TextMeshPro>();
            dialogTextMesh.text = dialogText;
        }
    }
}
