using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.SimpleLocalization;


public class ButtonLanguageCheck : MonoBehaviour
{

    [SerializeField] private string linguagem;
    [SerializeField] private Animator fade;
    private Text texto;

    private void Start()
    {
        texto = GetComponentInChildren<Text>();
    }
    private void Update()
    {
        if (LocalizationManager.Language == linguagem)
        {
            texto.fontStyle = FontStyle.Italic;
        }
        else
        {
            texto.fontStyle = FontStyle.Normal;
        }

    }


    private void OnMouseEnter()
    {

        LocalizationManager.Language = linguagem;

        //Debug.Log("yo yo yo" + linguagem);

    }


    public void confirm()
    {

        
        StartCoroutine(changeScene());
        
    }


    IEnumerator changeScene()
    {
        fade.SetTrigger("out");
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("Game");

    }

}
