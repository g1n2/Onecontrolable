using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable3D : MonoBehaviour,IInteract
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 pos,rot;
    [SerializeField] [Range(0,1)] private float speed;
    private Vector3 LastPos, toLerpPos, toLerpRot,LastRot;
    private bool canLerp;


    public void interact()
    {
        //Debug.Log("funcionou");


        LastPos = player.transform.localPosition;
        LastRot = new Vector3(player.transform.localRotation.x, player.transform.localRotation.y, player.transform.localRotation.z);

        StartCoroutine(lerp(pos,rot));

    }

    public void stopInteract()
    {
        LastPos -= player.transform.localPosition;
        //LastRot = player.transform.localRotation;

        StartCoroutine(lerp(LastPos, LastRot));
        LastPos = new Vector3(0,0,0);
        LastRot = new Vector3(0, 0, 0);
        
        

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (canLerp)
        {
            player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition,toLerpPos,speed); ;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation,
                                                                Quaternion.Euler(toLerpRot),
                                                                speed );
            //player.transform.rotation = rot;
        }


    }


    IEnumerator lerp(Vector3 pos, Vector3 rot)
    {

        toLerpPos = player.transform.localPosition + pos;
        
        toLerpRot = new Vector3(rot.x,rot.y,rot.z) ;     
        canLerp = true;
        yield return new WaitForSeconds(0.65f);
        canLerp = false;
        
    }

}
