using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
	private DialogueTrigger dialogueTriggerTest;

	void Start()
	{
		dialogueTriggerTest = GetComponent<DialogueTrigger>();
		
	}

	// When we interact with the enemy: We attack it.
	public override void Interact()
	{
		print("interact npc");
		dialogueTriggerTest.TriggerDialogue();
	}
}
