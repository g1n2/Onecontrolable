using UnityEngine;
using UnityEngine.Tilemaps;

public class CorrectionMap : MonoBehaviour
{
    [SerializeField] private LayerMask stop;
    private Transform[] tilemap;
    [Range(-1, 1)] [SerializeField] private float correctionValueX, correctionValueY;


    // Start is called before the first frame update
    void Start()
    {
        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();

        foreach (Tilemap tiles in tilemaps)
        {
            
            tiles.transform.localPosition = new Vector2(correctionValueX, correctionValueY);
        }

        GameObject[] objetoNaCamada = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objetoNaCamada)
        {
            if (obj.layer == LayerMask.NameToLayer("Unwalkable"))
            {
                // Debug.Log("objeto: "+obj.name);
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    Color color = renderer.color;
                    color.a = 0f;
                    renderer.color = color;
                }
            }
        }
        
        
    }


}
