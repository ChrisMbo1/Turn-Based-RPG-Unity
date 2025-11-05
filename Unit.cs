using UnityEngine;

public class Unit : MonoBehaviour
{

    //character  model

    public string unitName;
    public int dmg;

    public int unitLevel;

    public int currentHealth;

    public bool TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
    }
}
