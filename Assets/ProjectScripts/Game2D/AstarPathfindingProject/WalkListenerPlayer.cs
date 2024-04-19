using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkListenerPlayer : MonoBehaviour
{
    private PlayerMove PM;
    private PlayerHealth PH;


    private void Start()
    {
        PM = FindAnyObjectByType<PlayerMove>();
        PH = FindAnyObjectByType<PlayerHealth>();
    }
    // Start is called before the first frame update
    public void walkAnimIdleCheck()
    {
        PM.walkListener();
    }
    public void deathAnimIdleCheck()
    {
        PH.destroyChar();
    }


}
