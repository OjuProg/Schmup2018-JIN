﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAvatar : MonoBehaviour {

    // Standard Data.
    public float maxSpeed;
    public int maxHealth;
    public int health;

    // Events.
    public delegate void DeathAction(BaseAvatar baseAvatar);
    public static event DeathAction OnDeath;

    public void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        if (OnDeath != null)
        {
            OnDeath(this);
        }
    }
}