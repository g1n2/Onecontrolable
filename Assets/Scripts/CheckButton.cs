using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButton : MonoBehaviour
{


    [SerializeField] private PickUp[] pick;
    public PickUp occupant;
    public bool occupied;
    private PlayerMove PM;
    private PlayerATK PA;

    private void Start()
    {
         pick = GameObject.FindObjectsOfType<PickUp>();
         PM = GameObject.FindObjectOfType<PlayerMove>();
         PA = GameObject.FindObjectOfType<PlayerATK>();

    }

    private void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (pick[i].attached)
            {
                occupant = pick[i];
               
            }
           
        }

    }


    public void walk()
    {
        //Debug.Log("he did the thing");
        if (occupant == pick[0])
        {
            PM.walkR();
            //Debug.Log("R");
        }
        else if (occupant == pick[1])
        {
           StartCoroutine( PA.Attack());
            //Debug.Log("he");
        }
        else if (occupant == pick[2])
        {
            PM.walkD();
           // Debug.Log("D");
        }
        else if (occupant == pick[3])
        {
            PM.walkL();
            //Debug.Log("L");
        }
        else if (occupant == pick[4])
        {
            PM.walkU();
           // Debug.Log("U");
        }

    }




}
