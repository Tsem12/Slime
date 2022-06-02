using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [HideInInspector] public bool isItemDisplay;

    [SerializeField] private Text npcName;
    [SerializeField] private Text message;
    public GameObject buyButton;
    public Image image;
    private Queue<string> sentences;
    public Animator animator;
    private DialogueTriger npcTriger;
    private bool isUIActive;
    private bool isAttackDisable;
    public bool isDialogueFix;

    private void Update()
    {
        if (isUIActive && isDialogueFix)
        {
            GameManager.instance.isInputEnable = false;
            SwitchCharacter.instance.activeCharacter.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        else if (isUIActive && !isDialogueFix)
            GameManager.instance.isInputEnable = true;

        if (isUIActive)
        {
            SwitchCharacter.instance.activeCharacter.GetComponent<PlayerAttack>().canAttack = false;
            isAttackDisable = true;
        }
        else if(!isUIActive && isAttackDisable)
        {
            SwitchCharacter.instance.activeCharacter.GetComponent<PlayerAttack>().canAttack = true;
            isAttackDisable = false;
        }

        if(sentences.Count >= 0 && Input.GetKeyDown(KeyCode.F))
            Continue();

    }

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, DialogueTriger triger)
    {
        npcTriger = triger;

        animator.SetBool("isOpen", true);
        isUIActive = true;

        image.sprite = dialogue.image;
        npcName.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Continue();
    }

    public void Continue()
    {
        if(sentences.Count == 0)
        {
            npcTriger.ChooseAction(npcTriger.dialogue.functionToExecute);
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(DisplayMessage(sentence));
    }

    private IEnumerator DisplayMessage(string sentence)
    {
        message.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        isUIActive = false;
        GameManager.instance.isInputEnable = true;
    }

    public void BuyTriger()
    {
        npcTriger.GetComponent<BuyItems>().BuyItem();

    }
}
