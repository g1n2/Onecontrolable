using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MonoBehaviour
{
    private bool canWalk,canLerp,canSpin,isInteracting,canInteract;
    private Vector3 toGo,toSpin;
    [SerializeField] private string lookingDirection;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float walkDistance;


    public Transform checkFront,checkBack;
    [SerializeField] private LayerMask stopMove,interactable;
    [SerializeField] private float radius;

    // Start is called before the first frame update
    void Start()
    {
        lookingDirection = "F";
        canWalk = true;
        canInteract = true;
    }

    // Update is called once per frame
    void Update()
    {


        if (canLerp)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,toGo,walkSpeed);
        // transform.localPosition = Vector3.Lerp(transform.localPosition, toGo, walkSpeed);

        if (canSpin)
            transform.localRotation = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(toSpin), walkSpeed);
           

    

        if (isInteracting) {
            canSpin = false;
        }

        if (canWalk)
        {

                  
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !Physics.CheckSphere(checkFront.position, radius, stopMove))
            {
                StartCoroutine(move(walkDistance));
            }
            else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !Physics.CheckSphere(checkBack.position, radius, stopMove))
            {
                StartCoroutine(move(-walkDistance));
            }

            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(rotate(1));
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(rotate(-1));
            }


           
        }

        if (canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(interaction());              
            }
        }
            
    }


    void walk(float direction)
    {

 
      

            switch (lookingDirection)
            {

                case "F":
                    toGo = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - (-direction));

                    break;
                case "B":
                    toGo = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + (-direction));

                    break;

                case "L":
                    toGo = new Vector3(transform.localPosition.x + (-direction), transform.localPosition.y, transform.localPosition.z);

                    break;
                case "R":
                    toGo = new Vector3(transform.localPosition.x - (-direction), transform.localPosition.y, transform.localPosition.z);

                    break;
            }

        

        
    }



    void checarAngulo()
    {
        float yRotation = transform.localRotation.eulerAngles.y;

        bool olhandoDireita = yRotation > 45 && yRotation < 135;
        bool olhandoEsquerda = yRotation > 225 && yRotation < 315;
        bool olhandoTras = yRotation > 135 && yRotation < 225;
  
    if (olhandoEsquerda)
        {
           // Debug.Log("O objeto está olhando para a esquerda!");
            lookingDirection = "L";
        }
        else if (olhandoDireita)
        {
            //Debug.Log("O objeto está olhando para a direita!");
            lookingDirection = "R";
        } 
        else if (olhandoTras)
        {
           // Debug.Log("O objeto está olhando para trás!");
            lookingDirection = "B";
        }
        else
        {
            //Debug.Log("O objeto está olhando para frente!");
            lookingDirection = "F";

        }
    }

    IEnumerator interaction()
    {
        canInteract = false;

        Ray r = new Ray(gameObject.transform.position, gameObject.transform.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, 2))
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteract interactObj))
            {

                if (!isInteracting)
                {
                    interactObj.interact();
                    isInteracting = true;
                    canWalk = false;
                }
                else
                {
                    interactObj.stopInteract();
                    yield return new WaitForSeconds(1f);
                    isInteracting = false;
                    canWalk = true;
                }

            }
            yield return new WaitForSeconds(walkSpeed + 0.2f);
        canInteract = true;
    }

    IEnumerator move(float direction)
    {
        checarAngulo();
        canInteract = false;
        canWalk = false;
        canLerp = true;
        walk(direction);
        yield return new WaitForSeconds(walkSpeed + 0.2f);
        canWalk = true;
        canLerp = false;
        canInteract = true;
    }

    IEnumerator rotate(float direction)
    {
        canInteract = false;
        canWalk = false;
        canSpin = true;
        toSpin += new Vector3(roundTo10( transform.localRotation.x),
                                roundTo10(transform.localRotation.y-(-direction*90)),
                                roundTo10(transform.localRotation.z));
        yield return new WaitForSeconds(walkSpeed + 0.2f);
        canWalk = true;
        canSpin = false;
        canInteract = true;
    }

    float roundTo10(float i)
    {
        float value = i / 10;
        value = Mathf.Round(value);
        return value * 10;

    }

}


interface IInteract
{
    public void interact();
    public void stopInteract();
}
