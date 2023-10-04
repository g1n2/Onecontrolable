using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject[] walls;
    [SerializeField] private int roomVariance;


    [SerializeField] private bool[] testStatus;

  
    [SerializeField] private Tile coner1, coner2, coner3, coner4,sideT,sideB,sideL,sideR, blank;
    //c1 direita baixo, c2 esquerda baixo, c3 direita cima, c4 esquerda cima
    //[SerializeField] private Vector3Int pos1,pos2,pos3,pos4,pos5,pos6;
    [SerializeField] private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRoom(testStatus);


        
    }

    // Update is called once per frame
    void UpdateRoom(bool[] status)
    {
        for(int i = 0; i<status.Length; i++)
        {
            walls[i].SetActive(!status[i]);
            placeTile(status);
        }
    }

    void placeTile(bool[] status)
    {
        if (roomVariance == 0)
        {
            if (status[0])
            {
                replace(0, 2, coner3); //caminho direita
                replace(-2, 2, coner4);//caminho esquerda
                replace(-2, 3, sideL); //parede esquerda
                replace(0, 3, sideR); //parede direita
                replace(-1, 3, blank); //parede meio
                replace(-1, 2, blank); //caminho meio

            }
            if (status[1])
            {

                replace(0, -4, coner1); //caminho direita
                replace(-2, -4, coner2);//caminho esquerda
                replace(-2, -5, sideL);//parede esquerda
                replace(0, -5, sideR);//parede direita
                replace(-1, -4, blank); //caminho meio
                replace(-1, -5, blank);//parede meio

            }
            if (status[2])
            {

                replace(2, -2, coner1); //caminho baixo
                replace(2, 0, coner3); //caminho topo
                replace(3, 0, sideT); //parede Topo
                replace(3, -2, sideB); //parede baixo
                replace(2, -1, blank); //caminho meio
                replace(3, -1, blank); //parede meio

            }
            if (status[3])
            {
                replace(-4, 0, coner4); //caminho topo
                replace(-4, -2, coner2); //caminho baixo
                replace(-5, 0, sideT); //parede Topo
                replace(-5, -2, sideB); //parede Baixo
                replace(-5, -1, blank);//parede meio
                replace(-4, -1, blank);//caminho meio

            }
        }

        if (roomVariance==1)
        {
            if (status[0])
            {
                replace(0, 2, sideR); //caminho direita
                replace(-2, 2, sideL);//caminho esquerda
                replace(-2, 3, sideL); //parede esquerda
                replace(0, 3, sideR); //parede direita
                replace(-1, 3, blank); //parede meio
                replace(-1, 2, blank); //caminho meio
               

            }
            if (status[1])
            {

                replace(0, -4, sideR); //caminho direita
                replace(-2, -4, sideL);//caminho esquerda
                replace(-2, -5, sideL);//parede esquerda
                replace(0, -5, sideR);//parede direita
                replace(-1, -4, blank); //caminho meio
                replace(-1, -5, blank);//parede meio

            }
            if (status[2])
            {

                replace(2, -2, sideB); //caminho baixo
                replace(2, 0, sideT); //caminho topo
                replace(3, 0, sideT); //parede Topo
                replace(3, -2, sideB); //parede baixo
                replace(2, -1, blank); //caminho meio
                replace(3, -1, blank); //parede meio

            }
            if (status[3])
            {
                replace(-4, 0, sideT); //caminho topo
                replace(-4, -2, sideB); //caminho baixo
                replace(-5, 0, sideT); //parede Topo
                replace(-5, -2, sideB); //parede Baixo
                replace(-5, -1, blank);//parede meio
                replace(-4, -1, blank);//caminho meio

            }




        }

        if (roomVariance == 3)
        {
            if (status[0])
            {
                replace(0, 2, sideR); //caminho direita
                replace(-2, 2, sideL);//caminho esquerda
                replace(-2, 3, sideL); //parede esquerda
                replace(0, 3, sideR); //parede direita
                replace(-1, 3, blank); //parede meio
                replace(-1, 2, blank); //caminho meio

                replace(0, 1, coner3); //caminho dir
                replace(-1, 1, blank); //caminho meio
                replace(-2, 1, coner4); //caminho esq


            }
            if (status[1])
            {

                replace(0, -4, sideR); //caminho direita
                replace(-2, -4, sideL);//caminho esquerda
                replace(-2, -5, sideL);//parede esquerda
                replace(0, -5, sideR);//parede direita
                replace(-1, -4, blank); //caminho meio
                replace(-1, -5, blank);//parede meio

                replace(0, -3, coner1); //caminho dir
                replace(-1, -3, blank); //caminho meio
                replace(-2, -3, coner2); //caminho esq

            }
            if (status[2])
            {

                replace(2, -2, sideB); //caminho baixo
                replace(2, 0, sideT); //caminho topo
                replace(3, 0, sideT); //parede Topo
                replace(3, -2, sideB); //parede baixo
                replace(2, -1, blank); //caminho meio
                replace(3, -1, blank); //parede meio

                replace(1, 0, coner3); //caminho topo
                replace(1, -1, blank); //caminho meio
                replace(1, -2, coner1); //caminho baixo

            }
            if (status[3])
            {
                replace(-4, 0, sideT); //caminho topo
                replace(-4, -2, sideB); //caminho baixo
                replace(-5, 0, sideT); //parede Topo
                replace(-5, -2, sideB); //parede Baixo
                replace(-5, -1, blank);//parede meio
                replace(-4, -1, blank);//caminho meio

                replace(-3, 0, coner4); //caminho topo
                replace(-3, -1, blank); //caminho meio
                replace(-3, -2, coner2); //caminho baixo
            }




        }

    }


    void replace(int x, int y,Tile tile)
    {
        tilemap.SetTile(new Vector3Int(x, y, 0), tile);
    }

}
