using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* This class just makes it faster to get certain components on the player. */

public class Player : MonoBehaviour
{

	#region Singleton

	public static Player instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	void Start()
	{
		playerCombatManager = GetComponent<CharacterCombat>();
		playerStats = GetComponent<PlayerStats>();

		playerStats.OnHealthReachedZero += Die;
		playerLevel = GetComponent<PlayerLevel>();
	}

	private CharacterCombat playerCombatManager;
	private PlayerStats playerStats;
	private PlayerLevel playerLevel;


	void Die()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public CharacterCombat PlayerCombatManager
	{
		get { return playerCombatManager; }
	}

	public PlayerStats PlayerStats
	{
		get { return playerStats; }
	}

	public PlayerLevel PlayerLevel
	{
		get { return playerLevel; }
	}
}