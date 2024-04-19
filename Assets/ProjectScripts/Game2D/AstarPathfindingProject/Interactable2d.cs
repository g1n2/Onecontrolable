using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable2d : MonoBehaviour
{

    [SerializeField] private LayerMask controlLayer,ignoreLayer;
    [SerializeField]private CheckButton buttonChecker;
    [HideInInspector]public ButtonInfo buttonAttached;
    private ButtonInfo currentButton;

    // Update is called once per frame
    void Update()
    {

       

     

        if (Input.GetMouseButtonDown(0))
        {

            if (currentButton == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, controlLayer))
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    if (hit.transform.GetComponent<ButtonInfo>() == null)
                        return;
                  
                    currentButton = hit.transform.GetComponent<ButtonInfo>();
                    //Debug.Log("chaca");
                    
                        StartCoroutine(followMouse());
                      //currentButton.pickup();
                }
            }
            
        }

        if (Input.GetMouseButtonUp(0))
        {

 
            if (currentButton != null)
            {
                if (currentButton.attached)
                {                   
                    buttonChecker.Action(currentButton.buttonAction);
                    currentButton = null;
                    return;
                }
                currentButton.release();
                currentButton = null;
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
           
                if (buttonAttached.attached)
                {
                    buttonChecker.occupied = false;
                    buttonAttached.pop();
                    buttonAttached = null;
                }
            
        }

    }

    
    /*
    IEnumerator checkTime()
    {
        while (true)
        {
            timer = 0.3f;
            timer += Time.deltaTime;
            if (timer < 0.3)
            {
                buttonChecker.Action(currentButton.buttonAction);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                unClick();
                yield return new WaitForEndOfFrame();
            }
        }
    }
    */
    IEnumerator followMouse()
    {
        while (true)
        {
            if (currentButton != null)
            {
                if (!currentButton.attached)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ignoreLayer);
                    currentButton.transform.position = hit.point;


                    yield return new WaitForFixedUpdate();
                }
                    yield return new WaitForFixedUpdate();
            }
            else
                yield return new WaitForFixedUpdate();
        }
    }

}
