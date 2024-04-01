using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class F_DialogueManager : MonoBehaviour
{
    public static F_DialogueManager Instance { get; private set; }

    private bool inDialogue;
    private bool isTyping;
    private Queue<F_SO_Dialgue.Info> dialogueQueue;
    private string completeText;
    [SerializeField] private float textDelay = 0.1f; //how fast each letter shows up on screen
    //UI
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] GameObject dialogueBox;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        dialogueQueue = new Queue<F_SO_Dialgue.Info>();
    }

    private void OnInteract()
    {
        if (inDialogue)
        {
            DequeueDialogue();
        }
    }

    public void QueueDialogue(F_SO_Dialgue dialogue)
    {
        if (inDialogue)
        {
            return;
        }
        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = false;
        inDialogue = true;
        dialogueBox.SetActive(true);
        dialogueQueue.Clear();
        foreach (F_SO_Dialgue.Info line in dialogue.dialogueInfo)
        {
            dialogueQueue.Enqueue(line);
        }
        DequeueDialogue();
    }
    public void DequeueDialogue()
    {
        if (isTyping)
        {
            CompleteText();
            StopAllCoroutines();
            isTyping = false;
            return;
        }
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }
        F_SO_Dialgue.Info info = dialogueQueue.Dequeue();
        completeText = info.dialogue;
        dialogueText.text = "";
        StartCoroutine(TypeText(info));
    }
    private void CompleteText()
    {
        dialogueText.text = completeText;
    }
    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        inDialogue = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = true;
    }
    public IEnumerator TypeText(F_SO_Dialgue.Info info)
    {
        isTyping = true;
        foreach (char c in info.dialogue.ToCharArray())
        {
            yield return new WaitForSeconds(textDelay);
            dialogueText.text += c;
        }
        isTyping = false;
    }
}
