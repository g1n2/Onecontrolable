using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButton : MonoBehaviour
{
    
    
    public bool occupied;
    private PlayerMove PM;
    private PlayerATK PA;

    private void Start()
    {
         
         PM = GameObject.FindObjectOfType<PlayerMove>();
         PA = GameObject.FindObjectOfType<PlayerATK>();

    }

    public void Action(string act)
    {


        if (act == "Attack") 
        PA.StartCoroutine(act);
        else
        PM.Invoke(act,0);
    }




}
