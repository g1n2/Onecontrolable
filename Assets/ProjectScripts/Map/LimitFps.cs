using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFps : MonoBehaviour
{

    [SerializeField] private int fps;
    [SerializeField] private bool active;

    private void Update()
    {
        if (active)
            Application.targetFrameRate = fps;
    }


}
