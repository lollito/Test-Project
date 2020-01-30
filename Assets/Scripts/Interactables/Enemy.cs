using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This makes our enemy interactable. */

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{

	CharacterStats stats;

	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
		stats = GetComponent<CharacterStats>();
		stats.OnHealthReachedZero += Die;
		stats.OnHit += Hit;
		recoursiveInteraction = true;
	}

	// When we interact with the enemy: We attack it.
	public override void Interact()
	{
		//print("Interact");
		if (stats.isAlive()) {
			CharacterCombat combatManager = Player.instance.PlayerCombatManager;
			combatManager.Attack(stats);
		}
		
	}

	void Die()
	{
		animator.SetTrigger("die");
		//          mAnimator.SetBool ("shoot", false);
		//          mAnimator.SetBool ("attack", false);
		//          mAnimator.SetBool ("running", false);
		Player.instance.PlayerLevel.GrantExperience(50);
		CombatEvents.EnemyDied();
		StartCoroutine(Remove(3));
	}

	private void Hit()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Idle")) {
			animator.SetTrigger("hit");
		}
		
	}

	IEnumerator Remove(float time)
	{
		yield return new WaitForSeconds(time);

		Destroy(transform.gameObject);
	}
}
