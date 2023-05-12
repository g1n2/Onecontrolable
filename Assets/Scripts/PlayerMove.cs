using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float levelModifier = 0.2f;
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask stopMove;
    [SerializeField] private SpriteRenderer sprRSword, sprRSpear, sprRHammer;
    public bool canMove = true;
    private Animator anim;
    private SpriteRenderer sprR;
    private float directionH, directionV;
    private float walkCycle;
    private bool walkCycleBool;
    private PlayerATK PA;

    private Vector3 originalWeaponPos;
    [SerializeField] private Transform weapon;


    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        anim = GetComponent<Animator>();
        sprR = GetComponent<SpriteRenderer>();
        originalWeaponPos = new Vector3(weapon.localPosition.x,weapon.localPosition.y,weapon.localPosition.z);
        PA = GetComponent<PlayerATK>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 fixedMovePoint = new Vector3(movePoint.position.x, movePoint.position.y - 0.4356666f, movePoint.position.z);
        transform.position = Vector3.MoveTowards(transform.position, fixedMovePoint, speed * Time.deltaTime);

        if (canMove) {


            if (Vector3.Distance(transform.position, fixedMovePoint) == 0)
            {
                 
                if (Mathf.Abs(directionH) == 1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(directionH, 0, 0), 0f, stopMove))
                        movePoint.position += new Vector3(directionH +(levelModifier * directionH), 0, 0);

                }

                else if (Mathf.Abs(directionV) == 1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, directionV, 0), 0f, stopMove))
                        movePoint.position += new Vector3(0, directionV+(levelModifier * directionV), 0);
                }   
            }
           
        }
    }

    public void walkR()
    {

        directionH = 1;
        StartCoroutine(resetDir());
        StartCoroutine(WalkAnim("Side", false,false));

        if (walkCycle==1)       
             weapon.transform.localPosition = new Vector3(originalWeaponPos.x + 0.1f , originalWeaponPos.y, originalWeaponPos.z);
        else
            weapon.transform.localPosition = new Vector3(originalWeaponPos.x -0.2f, originalWeaponPos.y, originalWeaponPos.z);

    }

    public void walkL()
    {

        directionH = -1;
        StartCoroutine(resetDir());
        StartCoroutine(WalkAnim("Side", true,true));

        if (walkCycle == 1)
            weapon.transform.localPosition = new Vector3(originalWeaponPos.x - 0.1f, originalWeaponPos.y, originalWeaponPos.z);
        else
            weapon.transform.localPosition = new Vector3(originalWeaponPos.x + 0.2f, originalWeaponPos.y, originalWeaponPos.z);
    }

    public void walkU()
    {

        directionV = 1;
        StartCoroutine(resetDir());
        StartCoroutine(WalkAnim("Back", false,true));
    }

    public void walkD()
    {

        directionV = -1;
        StartCoroutine(resetDir());
       StartCoroutine( WalkAnim("Front", false,false));
       
    }

    IEnumerator WalkAnim(string direction, bool flip,bool flipWeapon)
    {
        PA.updateDirection(direction);
        anim.SetTrigger("Walk");
        anim.SetTrigger(direction);
        anim.SetFloat("Blend",walkCycle);
        changeFeet();
        sprR.flipX = flip;
        sprRHammer.flipX = flipWeapon;
        sprRSword.flipX = flipWeapon;
        sprRSpear.flipX = flipWeapon;
        yield return new WaitForSeconds(0.5f);       
        anim.SetTrigger("Idle");
        yield return new WaitForSeconds(0.1f);    
        anim.ResetTrigger("Idle");
        
    }



    public IEnumerator resetDir()
    {
        yield return new WaitForSeconds(0.1f);
        directionV = 0;
        directionH = 0;
    }

    void changeFeet()
    {

        walkCycleBool = !walkCycleBool;

        if (walkCycleBool)
        {
        walkCycle = 1;
        }
        else
        {
        walkCycle = 2;
        }

    }

    public void walkListener()
    {
        weapon.transform.localPosition = new Vector3(originalWeaponPos.x, originalWeaponPos.y, originalWeaponPos.z);
    }

}
