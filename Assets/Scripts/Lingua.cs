using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;

public class Lingua : MonoBehaviour
{

    


    private void Awake()
    {

        LocalizationManager.Read();


       
        
        if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            LocalizationManager.Language = "Portugues";
        }
        else
        {
            LocalizationManager.Language = "English";
        }
        
    }

    




}
