using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField]
    public DialogueSet set;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            DialogueSystem.instance.StartDialogue(set);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            DialogueSystem.instance.EndDialogue(); 
        }
    }
}
