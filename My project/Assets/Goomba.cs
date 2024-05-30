using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : DamagingTypes
{
    [SerializeField] private float hastighet = 6f; // Goombas hastighet, SerializeField så att den kan justeras i Unity samtidigt som den är private
    [SerializeField] private float sträcka = 2f; //Sträckan som Goomba går, SerializeField så att den kan justeras i Unity samtidigt som den är private

    private Vector2 positionA; //Den första änden av sträckan som Goomba rör sig på
    private Vector2 positionB; //Den andra änden av sträckan som Goomba rör sig på
    private bool movingRight = true; //Håller koll på vilket håll Goomba rör sig mot
    private float rörelseKonstant = 0.1f;

    void Start()
    {
        positionA = transform.position; //Sätter den första änden av sträckan som Goomba rör sig på till positionA 
        positionB = positionA + Vector2.right * sträcka; //Sätter den andra änden av sträckan som Goomban rör sig på till positionB
    }

    void Update()
    {
        Move(); //Kallar metoden för att få Goomba och röra på sig
    }

    // Metod för att flytta Goomba
    void Move()
    {
        float step = hastighet * Time.deltaTime; //Beräknar längden av Goombas rörelse

        Vector2 newPosition = Vector2.MoveTowards(transform.position, movingRight ? positionB : positionA, step); //Beräknar den nya positionen för Goomba
        newPosition.y = positionA.y; //Gör så att Goomba inte kan röra sig vertikalt.

        GetComponent<Rigidbody2D>().MovePosition(newPosition); //Flyttar Goomba till den nya positionen

        
        if (Vector2.Distance(transform.position, movingRight ? positionB : positionA) < rörelseKonstant) //Ändrar Goombas riktning
        {
            movingRight = !movingRight; 
        }
    }
}