using System;
using System.Collections;using System.Collections.Generic;using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour{    // List of the objects in the inventory    private GameObject[] arrayOfObjects;    // Size of the inventory    public int inventorySize = 5;

    private void Awake()    {        arrayOfObjects = new GameObject[inventorySize];        for (int i = 0; i < inventorySize; i++)            arrayOfObjects[i] = null;    }

    /**    * Adds an object to the objects in the inventory.    *     * @param pickedUpObject    Object picked to be added to the inventory.    * @return true if the object can be added, false otherwise    */
    public bool AddObject(GameObject pickedUpObject)    {        Debug.Log(pickedUpObject.name);        for (int i = 0; i < inventorySize; i++)
        {
            if (arrayOfObjects[i] == null)
            {
                GameObject slot = transform.Find("Slot" + i).Find("Object").gameObject;
                Image objectImage = slot.GetComponent<Image>();
                SpriteRenderer spritePickedUpObject = pickedUpObject.GetComponent<SpriteRenderer>();
                objectImage.color = spritePickedUpObject.color;
                objectImage.sprite = spritePickedUpObject.sprite;
                objectImage.name += spritePickedUpObject.name;

                arrayOfObjects[i] = pickedUpObject;
                return true;
            }
        }        return false;    }    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out hit, Mathf.Infinity);

            Debug.Log("Button down: isHit? " + isHit);
            if (isHit)
            {
                GameObject hitObject = hit.transform.gameObject;
                AddObject(hitObject);
                hitObject.SetActive(false);

                DialogueSystem.Instance.SetDialogue(hitObject.GetComponent<PickupObjects>().dialogText);
            }
        }
    }

    public bool RemoveObject(GameObject objectToRemove)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (arrayOfObjects[i] == objectToRemove)
            {
                arrayOfObjects[i] = null;
                return true;
            }
        }
        return false;
    }
}