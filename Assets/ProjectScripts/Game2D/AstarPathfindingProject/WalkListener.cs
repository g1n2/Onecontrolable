using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkListener : MonoBehaviour
{
   [SerializeField] private EnemyMove EM;
   [SerializeField] private EnemyHealth EH;
   [HideInInspector] public bool U,D,L,R;
   public void walk()
    {

        if (U)
        {
            EM.walkU();
            U = false;
        }
        if (D)
        {
            EM.walkD();
            D = false;
        }
        if (L)
        {
            EM.walkL();
            L = false;
        }
        if (R)
        {
            EM.walkR();
            R = false;
        }
    }
    public void atk()
    {
        EM.damage();
    }

    public void die()
    {
        EH.delete();
    }

}
