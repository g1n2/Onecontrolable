using UnityEngine;

public class correctionMap : MonoBehaviour
{
    [SerializeField] private LayerMask stop;
    [SerializeField] private Transform tilemap;
    [Range(-1, 1)] [SerializeField] private float correctionValueX, correctionValueY;


    // Start is called before the first frame update
    void Start()
    {
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
        tilemap.position = new Vector2(correctionValueX, correctionValueY);
    }


}
