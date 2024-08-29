using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorListener : MonoBehaviour
{

    public void open()
    {

        
        FindAnyObjectByType<AudioManager>().stop("Music02");
        FindAnyObjectByType<AudioManager>().play("Music01");
    }
    public void close()
    {
        FindAnyObjectByType<AudioManager>().stop("Music01");
        FindAnyObjectByType<AudioManager>().play("Music02");
    }


}
