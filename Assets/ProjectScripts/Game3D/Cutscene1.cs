using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1 : MonoBehaviour
{
    [SerializeField] private List<GameObject> materialList;
    [SerializeField] private List<GameObject> objetoExcecao;
    [SerializeField] private PSXShaderKit.PSXPostProcessEffect script; 
    private Material material;

    // Start is called before the first frame update
    void Start()
    {

        material = new Material(Shader.Find("PSX/Vertex Lit"));

        GameObject[] todosObjetos = FindObjectsOfType<GameObject>();

        // Itera sobre todos os objetos
        foreach (GameObject obj in todosObjetos)
        {
            // Verifica se o objeto é o objeto de exceção
            if (objetoExcecao.Contains(obj))
            {
                continue; // Ignora este objeto e passa para o próximo
            }

            // Verifica se o objeto possui um material
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Verifica se o material do objeto é um standard shader
                if (renderer.sharedMaterial.shader.name.Contains("Standard"))
                {
                    // Adiciona o objeto à lista
                    materialList.Add(obj);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (GameObject i in materialList)
            {
                i.GetComponent<Renderer>().material = material;
                
            }
            script.enabled = true;
        }


    }
}
