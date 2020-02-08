﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryInteractor : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private Vector2 originalPosition;
    private bool originPosSet;

    private static List<RectTransform> AllInventoeyItems = new List<RectTransform>();

    public PlayMakerFSM MainFSM;

    void Start()
    {
        MainFSM = FindObjectOfType<PlayMakerFSM>();
        if (AllInventoeyItems.Count != 0) return;
        AllInventoeyItems = FindObjectsOfType<RectTransform>().ToList();
        AllInventoeyItems = (from content in AllInventoeyItems
                            where content.name.Contains("Object")
                            select content).ToList();
        //foreach (var item in AllInventoeyItems)
        //{
        //    Debug.Log(item.name, item);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
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
        foreach (var item in AllInventoeyItems)
        {
            bool interset = itemInteraction(GetComponent<RectTransform>(), item);
            if (interset)
            {
                int combination = 0;
                if (name == "SultanHat" && item.name == "Lamp") combination = 1;
                if (name == "Hammer" && item.name == "Instructions") combination = 2;
                if (name == "AstronautHelmet" && item.name == "IdCard") combination = 3;
                MainFSM.Fsm.Event(combination.ToString());

            }
        }
        originPosSet = false;
        transform.position = originalPosition;
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
