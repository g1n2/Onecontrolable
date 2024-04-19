using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingCharacters : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float levelModifier = 0.2f; //modificador para centralizar o personagem na
                                                         //cena dependendo do tamanho da grid
                                                         //(nesse caso a grid é 1.2, então o modificador é +0.2)
    public Transform movePoint;
    [SerializeField] public LayerMask stopMove;
    public bool canMove = true;
    private float directionH, directionV;

    


    // Start is called before the first frame update
     void Awake()
    {

        movePoint.parent = null;
        
    }

    // Update is called once per frame
    public void gridMovement()
    {


        Vector3 fixedMovePoint = new Vector3(movePoint.position.x, movePoint.position.y - 0.4f, movePoint.position.z);
        transform.position = Vector3.MoveTowards(transform.position, fixedMovePoint, speed * Time.deltaTime);

        if (canMove)
        {


            if (Vector3.Distance(transform.position, fixedMovePoint) == 0)
            {

                if (Mathf.Abs(directionH) == 1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(directionH, 0, 0), 0f, stopMove))
                        movePoint.position += new Vector3(directionH + (levelModifier * directionH), 0, 0);

                }

                else if (Mathf.Abs(directionV) == 1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, directionV, 0), 0f, stopMove))
                        movePoint.position += new Vector3(0, directionV + (levelModifier * directionV), 0);                  
                }



            }
        }
    }



    public virtual void walkR()
    {
        directionH = 1;
        StartCoroutine(resetDir());
    }

    public virtual void walkL()
    {
        
        directionH = -1;
        StartCoroutine(resetDir());
    }

    public virtual void walkU()
    {
        directionV = 1;
        StartCoroutine(resetDir());
    }

    public virtual void walkD()
    {
      
        directionV = -1;
        StartCoroutine(resetDir());
    }

    public IEnumerator resetDir()
    {
        yield return new WaitForSeconds(0.1f);
        directionV = 0;
        directionH = 0;
    }
}
