using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Tree : Interactable
{

	CharacterStats stats;

	void Start()
	{
		//animator = GetComponent<Animator>();
		stats = GetComponent<CharacterStats>();
		stats.OnHealthReachedZero += Die;
		
		recoursiveInteraction = true;
	}

	// When we interact with the enemy: We attack it.
	public override void Interact()
	{
		if (stats.isAlive())
		{
			CharacterCombat combatManager = Player.instance.PlayerCombatManager;
			combatManager.Chop(stats);
		}

	}

	void Die()
	{
		
		Player.instance.PlayerLevel.GrantExperience(50);

		Destroy(transform.gameObject);
	}

}
