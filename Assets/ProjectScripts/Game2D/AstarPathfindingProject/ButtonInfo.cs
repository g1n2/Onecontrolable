using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    [Header("misc")]
    [SerializeField] public string buttonAction;
    [SerializeField] private Interactable2d I2;

    [Header("Area de Prender o Btn")]
    [SerializeField] private GameObject area;
    [SerializeField] private Vector3 offset;

    [Header("Clamps")]
    [SerializeField] private float xClampMax;
    [SerializeField] private float xClampMin, yClampMax, yClampMin, zClampMax, zClampMin;


    private CheckButton CB;
    private bool onCollider;
    public bool attached;



    // Start is called before the first frame update
    void Start()
    {
        CB = area.GetComponent<CheckButton>();
    }

    // Update is called once per frame
    void Update()
    { 
        
        
       transform.localPosition = new Vector3(
               Mathf.Clamp(transform.localPosition.x, xClampMin, xClampMax),
               Mathf.Clamp(transform.localPosition.y, yClampMin, yClampMax),
               Mathf.Clamp(transform.localPosition.z, zClampMin, zClampMax)
               );
        
        if (attached)
        {
            //CB.occupied = true;
            transform.localPosition = Vector3.MoveTowards(
                   transform.localPosition,
                   new Vector3(area.transform.localPosition.x, area.transform.localPosition.y, area.transform.localPosition.z)+offset,
                   0.5f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(-90, 180, 0), 0.5f);
            GetComponent<Rigidbody>().angularVelocity = Vector3.back*0;
        }


        }



    
    public void release()
    {

            if (!onCollider)
            {
                pop();
                return;
               
            }
            if (!CB.occupied)
            {
            //Debug.Log("debug test 1");
                I2.buttonAttached = gameObject.GetComponent<ButtonInfo>();
                attached = true;
                GetComponent<Rigidbody>().useGravity = false;
                CB.occupied = true;
            }
            else
            {
                //Debug.Log("debug test 2");
                pop();
            }
     
    }

    public void pop()
    {
        attached = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-0.5f, 0.5f), 0.5f, 0.5f);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == area)
        {
            onCollider = true;        
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == area)
        {
            onCollider = false;
        }
    }

    

}
