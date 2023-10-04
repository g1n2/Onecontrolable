using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MovingCharacters
{

    [SerializeField] private SpriteRenderer sprRHammer, sprRSword, sprRSpear;
    [SerializeField] private Animator swordAnim,spearAnim,hammerAnim;
    [SerializeField] private Transform weapon;
    private Animator anim;
    private SpriteRenderer sprR; 
    private float walkCycle;
    private bool walkCycleBool;
    private PlayerATK PA;
    private Vector3 originalWeaponPos;


    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        sprR = GetComponent<SpriteRenderer>();
        originalWeaponPos = new Vector3(weapon.localPosition.x,weapon.localPosition.y,weapon.localPosition.z);
        PA = GetComponent<PlayerATK>();
        
    }

    private void Update()
    {
        gridMovement();
    }

    public override void walkR()
    {
        base.walkR();
        StartCoroutine(resetDir());
        StartCoroutine(WalkAnim("Side", false,false));

        if (walkCycle==1)       
             weapon.transform.localPosition = new Vector3(originalWeaponPos.x + 0.1f , originalWeaponPos.y, originalWeaponPos.z);
        else
            weapon.transform.localPosition = new Vector3(originalWeaponPos.x -0.2f, originalWeaponPos.y, originalWeaponPos.z);

    }

    public override void walkL()
    {

        base.walkL();
 
        StartCoroutine(WalkAnim("Side", true,true));

        if (walkCycle == 1)
            weapon.transform.localPosition = new Vector3(originalWeaponPos.x - 0.1f, originalWeaponPos.y, originalWeaponPos.z);
        else
            weapon.transform.localPosition = new Vector3(originalWeaponPos.x + 0.2f, originalWeaponPos.y, originalWeaponPos.z);
    }

    public override void walkU()
    {

        base.walkU();


        
        StartCoroutine(WalkAnim("Back", false,true));
    }

    public override void walkD()
    {

        base.walkD();

       StartCoroutine( WalkAnim("Front", false,false));
       
    }
    
    IEnumerator WalkAnim(string direction, bool flip,bool flipWeapon)
    {
        swordAnim.SetTrigger("go");
        spearAnim.SetTrigger("go");
        hammerAnim.SetTrigger("go");
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
        swordAnim.SetTrigger("back");
        spearAnim.SetTrigger("back");
        hammerAnim.SetTrigger("back");
        anim.SetTrigger("Idle");
        yield return new WaitForSeconds(0.1f);    
        anim.ResetTrigger("Idle");
        
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
