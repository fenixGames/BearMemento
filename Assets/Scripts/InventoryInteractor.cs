using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryInteractor : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private Vector2 originalPosition;
    private bool originPosSet;
    private static Color original = new Color(255, 255, 255, 0);

    private static List<RectTransform> AllInventoryItems = new List<RectTransform>();

    public PlayMakerFSM MainFSM;

    void Start()
    {
        MainFSM = FindObjectOfType<PlayMakerFSM>();
        if (AllInventoryItems.Count != 0) return;
        AllInventoryItems = FindObjectsOfType<RectTransform>().ToList();
        AllInventoryItems = (from content in AllInventoryItems
                            where content.name.Contains("Object")
                            select content).ToList();
        //foreach (var item in AllInventoeyItems)
        //{
        //    Debug.Log(item.name, item);
        //}
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (!originPosSet)
        {
            originPosSet = true;
            originalPosition = transform.position;
        }
        transform.parent.SetSiblingIndex(transform.parent.parent.childCount);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (var item in AllInventoryItems)
        {
            bool interset = itemInteraction(GetComponent<RectTransform>(), item);
            if (interset)
            {
                print("running");
                int combination = 0;
                if (name.Contains("SultanHat") && item.name.Contains("Lamp")) combination = 1;
                if (item.name.Contains("SultanHat") && name.Contains("Lamp")) combination = 1;
                if (name.Contains("Wood") && item.name.Contains("Instructions")) combination = 2;
                if (item.name.Contains("Wood") && name.Contains("Instructions")) combination = 2;
                if (name.Contains("AstronautHelmet") && item.name.Contains("IdCardWithPicture")) combination = 3;
                if (item.name.Contains("AstronautHelmet") && name.Contains("IdCardWithPicture")) combination = 3;
                if (name.Contains("Hammer") && item.name.Contains("Box")) combination = 4;
                if (item.name.Contains("Hammer") && name.Contains("Box")) combination = 4;
                if (name.Contains("PolaroidPhoto") && item.name.Contains("IdCard")) combination = 5;
                if (item.name.Contains("PolaroidPhoto") && name.Contains("IdCard")) combination = 5;

                
                MainFSM.Fsm.Event(combination.ToString());
                print(name + " " + item.name);
                print(combination);

                if (combination != 0)
                {
                    ResetObject(item.gameObject);
                    ResetObject(gameObject);
                }
            }
        }
        originPosSet = false;
        transform.position = originalPosition;
    }

    private void ResetObject(GameObject objectToReset)
    {
        /**
         * GameObject slot = transform.Find("Slot" + listOfObjects.Count).Find("Object").gameObject;            Image objectImage = slot.GetComponent<Image>();            SpriteRenderer spritePickedUpObject = pickedUpObject.GetComponent<SpriteRenderer>();
            objectImage.color = spritePickedUpObject.color;            objectImage.sprite = spritePickedUpObject.sprite;            objectImage.name += spritePickedUpObject.name;
        */
        Image objectImage = objectToReset.GetComponent<Image>();
        objectImage.color = original;
        objectImage.sprite = null;
        objectImage.name = "Object";
    }

    public void ShowItems(string[] items)
    {
        foreach (var itemName in items) {
            foreach (var item in AllInventoryItems)
            {
                if (item.name.Contains(itemName))
                    item.gameObject.SetActive(true);
            }
        }
    }

    bool itemInteraction(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        if (rectTrans1.Equals(rectTrans2))
        {
            return false;
        }
        float dist = Vector3.Distance(rectTrans1.position, rectTrans2.position);
        if (dist < 50) return true;
        return false;
    }
}
