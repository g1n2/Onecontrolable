using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable3D : MonoBehaviour,IInteract
{
    /*
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 pos,rot,collPos;
    [SerializeField] [Range(0,1)] private float speed;
    private Vector3 LastPos,LastRot, toLerpPos, toLerpRot,collOriginalPos;
    private bool canLerp;
    private BoxCollider coll;

    public void interact()
    {
        //Debug.Log("funcionou");


        LastPos = player.transform.localPosition;
        LastRot = new Vector3(player.transform.localRotation.x, player.transform.localRotation.y, player.transform.localRotation.z);
       // LastRot = new Vector3(player.transform.localRotation.x, player.transform.localRotation.y, player.transform.localRotation.z);
        coll.center = collPos;

        StartCoroutine(lerp(pos,rot));

    }

    public void stopInteract()
    {
        LastPos -= player.transform.localPosition;
        //LastRot = player.transform.localRotation;
        coll.center = collOriginalPos;

        StartCoroutine(lerp(LastPos, LastRot));
        LastPos = new Vector3(0,0,0);
        LastRot = new Vector3(0, 0, 0);
        
        

    }


    // Start is called before the first frame update
    void Start()
    {
        coll = gameObject.GetComponent<BoxCollider>();
        collOriginalPos = coll.center;
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
    */
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 pos, rot, collPos;
    [SerializeField] [Range(0, 1)] private float speed;
    [SerializeField] private float fov = 60;
    private float originalFov;
    private Vector3 toLerpPos, collOriginalPos, originalPlayerRotation;

    private BoxCollider coll;

    private void Start()
    {
        originalFov = Camera.main.fieldOfView;
        coll = gameObject.GetComponent<BoxCollider>();
        collOriginalPos = coll.center;
    }

    public void interact()
    {
       
        toLerpPos = pos;
        toLerpPos += player.transform.localPosition;
        Quaternion toLerpRot = Quaternion.Euler(rot);
        coll.center = collPos;
        originalPlayerRotation = player.transform.localRotation.eulerAngles;

        StartCoroutine(Lerp(player.transform, toLerpPos, toLerpRot,fov));
    }

    public void stopInteract()
    {

        toLerpPos = -pos;
        toLerpPos += player.transform.localPosition;
        coll.center = collOriginalPos;

        StartCoroutine(Lerp(player.transform, toLerpPos, Quaternion.Euler(originalPlayerRotation),originalFov));
    }

    private IEnumerator Lerp(Transform transform, Vector3 targetPosition, Quaternion targetRotation, float fovCam)
    {
      
        float startTime = Time.time;
        Vector3 startPosition = transform.localPosition;
        Quaternion startRotation = transform.localRotation;

        while (Time.time < startTime + speed)
        {
            float t = (Time.time - startTime) / speed;
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,fovCam,t);
            yield return null;
        }

        transform.localPosition = targetPosition;
        transform.localRotation = targetRotation;
    
    }
}
