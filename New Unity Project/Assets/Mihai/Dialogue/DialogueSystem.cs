using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public static DialogueSystem instance;
    public GameObject dialogUI;
    public Text characterLine;
    public Image characterImage;
    private Animator _animator;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start () {
        _animator = dialogUI.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartDialogue(DialogueSet pSet)
    {
        characterLine.text = pSet.characterName + '\n' + pSet.line;
        characterImage.sprite = pSet.characterImage;
        _animator.SetBool("isOnScreen", true);
    }

    public void EndDialogue()
    {
        _animator.SetBool("isOnScreen", false);
    }
}
