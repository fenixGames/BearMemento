using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    public List<Dialogue> AllDialogues;
    public TextMeshProUGUI DialogueText;


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        AssignDialogue("Initial");

    }

    public void AssignDialogue(string dialogueName)
    {
        Dialogue selected = AllDialogues.FirstOrDefault(content => content.DialogueName == dialogueName);
        DialogueText.text = selected.Text;
    }

}
    [Serializable]
    public class Dialogue
    {
        public string DialogueName;
        [TextArea(5, 8)]
        public string Text;
    }
