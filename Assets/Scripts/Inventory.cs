using System;
using System.Collections;using System.Collections.Generic;using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour{    // List of the objects in the inventory    private List<GameObject> listOfObjects;    // Size of the inventory    public int inventorySize = 5;

    private void Awake()    {        listOfObjects = new List<GameObject>();    }

    /*** Adds an object to the objects in the inventory.* * @param pickedUpObject    Object picked to be added to the inventory.* @return true if the object can be added, false otherwise*/
    public bool AddObject(GameObject pickedUpObject)    {        if (listOfObjects.Count < inventorySize)        {            GameObject slot = transform.Find("Slot" + listOfObjects.Count).Find("Object").gameObject;            Image objectImage = slot.GetComponent<Image>();            SpriteRenderer spritePickedUpObject = pickedUpObject.GetComponent<SpriteRenderer>();
            objectImage.color = spritePickedUpObject.color;            objectImage.sprite = spritePickedUpObject.sprite;            objectImage.name += spritePickedUpObject.name;            listOfObjects.Add(pickedUpObject);            return true;        }        return false;    }    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Button pushed");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject hitObject = hit.transform.gameObject;
                AddObject(hitObject);
                hitObject.SetActive(false);

                DialogueSystem.Instance.SetDialogue(hitObject.GetComponent<PickupObjects>().dialogText);
            }
        }
    }


}