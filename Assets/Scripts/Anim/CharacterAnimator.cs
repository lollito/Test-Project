using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{

	private Animator animator;

	NavMeshAgent navmeshAgent;
	CharacterCombat combat;

	protected virtual void Start()
	{
		navmeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		combat = GetComponent<CharacterCombat>();
		combat.OnAttack += OnAttack;
		combat.OnChop += OnChop;
	}

	protected virtual void Update()
	{
		animator.SetFloat("speedPercent", navmeshAgent.velocity.magnitude / navmeshAgent.speed, .1f, Time.deltaTime);
	}

	protected virtual void OnAttack()
	{
		animator.SetTrigger("punch");
	}

	protected virtual void OnChop()
	{
		animator.SetTrigger("chop");
	}

	public void disableAttack()
	{
		animator.ResetTrigger("arrow");
	}

	public bool isOnAttack() 
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName("Shooting Arrow");
	}
}
