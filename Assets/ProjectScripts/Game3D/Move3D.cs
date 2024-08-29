using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MonoBehaviour
{
    [Header("Andar")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] public float walkDistance;
    [SerializeField] private Animator anim;
    public float currentTurnSpeed, currentWalkSpeed;
    private string lookingDirection;
    private bool canWalk,isInteracting,canInteract;
    private Vector3 toGo,toSpin;



    [Header("Check de Parede")]  
    [SerializeField] private float radius;
    [SerializeField] private LayerMask stopMove,interactable;
    public Transform checkFront,checkBack;
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
        /*
        if (canLerp)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,toGo,walkSpeed);
        // transform.localPosition = Vector3.Lerp(transform.localPosition, toGo, walkSpeed);

        if (canSpin)
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation,Quaternion.Euler(toSpin), 5f);
        */

        bool chased = false;


        if (chased)
        {
            currentWalkSpeed = walkSpeed * 2;
            currentTurnSpeed = turnSpeed * 2;
        }
        else
        {
            currentWalkSpeed = walkSpeed;
            currentTurnSpeed = turnSpeed;
        }



        if (canWalk)
        {
            if (Input.GetKeyDown(KeyCode.W) && !Physics.CheckSphere(checkFront.position, radius, stopMove))
            {
                StartCoroutine(move(walkDistance));
            }
            else if (Input.GetKeyDown(KeyCode.S) && !Physics.CheckSphere(checkBack.position, radius, stopMove))
            {
                StartCoroutine(move(-walkDistance));
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(rotate(90));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(rotate(-90));
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

    //qual a direcao q ele tem q se mover
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


    //checa qual direcao ele esta olgando
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

    //quando ele interage com algo
    IEnumerator interaction()
    {
        canInteract = false;

        //Debug.Log("pressed e");
        //Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward,Color.red,2);

        Ray r = new Ray(gameObject.transform.position, gameObject.transform.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, 2))
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteract interactObj))
            {
               // Debug.Log("detected");
                if (!isInteracting)
                {
                    interactObj.interact();
                    //todo: mudar isso
                    isInteracting = true;
                    canWalk = false;
                }
                else
                {
                    interactObj.stopInteract();
                    yield return new WaitForSeconds(1f);
                    //todo: mudar isso
                    isInteracting = false;
                    canWalk = true;
                }

            }
            yield return new WaitForSeconds(walkSpeed + 0.2f);
        canInteract = true;
    }

    //pra onde ele tem q ir
    IEnumerator move(float direction)
    {
        checarAngulo();
        canInteract = false;
        canWalk = false;
        //canLerp = true;
        walk(direction);

        while (Vector3.Distance(transform.localPosition, toGo) > 0.01f)
        {
            // Move o personagem em direção ao destino com a velocidade suavizada pelo tempo.
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, toGo, currentWalkSpeed);
            anim.SetBool("walk", true);
            yield return null;
        }
        anim.SetBool("walk", false);
        canWalk = true;
        canInteract = true;
    }

    //direcao q ele gira
    IEnumerator rotate(float direction)
    {
        canInteract = false;
        canWalk = false;
        toSpin += new Vector3(roundTo10( transform.localRotation.x),
                             roundTo10(transform.localRotation.y+direction),
                             roundTo10(transform.localRotation.z));
        while (Quaternion.Angle(transform.rotation,Quaternion.Euler (toSpin))>0.1f )
        {
            transform.localRotation = Quaternion.RotateTowards
                (transform.localRotation, Quaternion.Euler(toSpin), currentTurnSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(walkSpeed + 0.2f);
        canWalk = true;
        canInteract = true;
    }

    //faz o personagem "nao sair dos trilhos"
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
