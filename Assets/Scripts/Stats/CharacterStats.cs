using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHealth;          // Maximum amount of health
    public int currentHealth { get; protected set; }   // Current amount of health

    public Stat damage;
    public Stat armor;

    //public Image healthBar;

    

    public event System.Action OnHealthReachedZero;

    public event System.Action<int, int> OnHealthChanged;

    public event System.Action OnHit;

    public void Awake()
    {
        currentHealth = maxHealth.GetValue();
    }

    //  void Update(){
    //      if(Input.GetKeyDown(KeyCode.T)){
    //          TakeDamage (10);
    //      }
    //  }

    public virtual void Start()
    {

    }

    // Damage the character
    public void TakeDamage(int damage)
    {
        // Subtract the armor value - Make sure damage doesn't go below 0.
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // Subtract damage from health
        currentHealth -= damage;

        // If we hit 0. Die.
        if (currentHealth <= 0)
        {
            //TODO die
            currentHealth = 0;
            OnHealthReachedZero?.Invoke();
        }
        else {
            OnHit?.Invoke();
        }

        OnHealthChanged?.Invoke(maxHealth.GetValue(), currentHealth);
        //healthBar.fillAmount = (float)currentHealth / (float)maxHealth.GetValue();
    }


    // Heal the character.
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());
    }

    public bool isAlive() 
    {
        return currentHealth > 0;
    }
    
}
