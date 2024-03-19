using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charsHP : MonoBehaviour
{

    [SerializeField] public float health = 3;

    public void loseHP(float damage)
    {
        health -= damage;
        Debug.Log("vida: " + health);
    }

    public void gainHP(float heal)
    {
        health += heal;
        Debug.Log("vida: " + health);
    }

}
