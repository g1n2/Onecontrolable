using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : charsHP
{

    private void FixedUpdate()
    {

        death();

    }
    void death()
    {
        if (health <= 0)
        {
            if (anim != null)
            {
                anim.Play("Die");

            }

        }
    }

    public void delete()
    {
        Destroy(gameObject);
    }
    
}
