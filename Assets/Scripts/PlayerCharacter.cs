using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int maxHealth = 5;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void FirstAid(int healthAdded)
    {
        health += healthAdded;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        float healthPercent = ((float)health) / maxHealth;
        Messenger<float>.Broadcast(GameEvents.HEALTH_CHANGED, healthPercent);
    }

    public void Hit()
    {
        health -= 1;
        Messenger<float>.Broadcast(GameEvents.HEALTH_CHANGED, ((float)health / 4));
        Debug.Log("Health: " + health);
        if (health == 0)
        {
            // Debug.Break(); // pauses game!
            Messenger.Broadcast(GameEvents.PLAYER_DEAD);
        }
    }
}
