using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Rorelse : MonoBehaviour
{
    private float sidleds; 
    [SerializeField] private float hastighet = 4f; //Marios hastighet, SerializeField så att den kan justeras i Unity samtidigt som den är private
    [SerializeField] private float hopp = 16f; //Marios hopphastighet, SerializeField så att den kan justeras i Unity samtidigt som den är private
    private bool hoger = true;
    [SerializeField] private float radieMario = 0.2f; //Radius för Marios collider, SerializeField så att den kan justeras i Unity samtidigt som den är private
    [SerializeField] private float fallHastighet = 0.5f; //Marios hastighet för att falla ned, SerializeField så att den kan justeras i Unity samtidigt som den är private
    
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private Transform rorMarken; //Kontrollerar om Mario rör marken eller inte
    [SerializeField] private LayerMask marken; //Layer för marken, definerar vad som är marken

    //Andra scripts
    [SerializeField] private gameOverScript gameOverScreen; 
    [SerializeField] private coinHanterare ch;

    void Update()
    {   
        sidleds = Input.GetAxisRaw("Horizontal"); //Tar kontrollerna från Unity som används för att röra sig sidleds
       
        if (Input.GetButtonDown("Jump") && nuddarMarken())  //Gör så att man bara kan hoppa när man nuddar marken
        {   
            rb.velocity = new Vector2(rb.velocity.x, hopp); //Räknar ut hastigheten för hoppet genom att använda rigidbody komponenten för Mario i Unity
        }
         
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) //Saktar ner hastigheten gradvist när mario hoppar för smidighet 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fallHastighet);
        }

        bytHall();
    }

    //Uppdaterar rigidbox hastigheten baserat på sidleds rörelse
    private void FixedUpdate()
    {  
        rb.velocity = new Vector2(sidleds * hastighet, rb.velocity.y); 
    }

    //Kollar om Mario nuddar marken
    private bool nuddarMarken()
    {
        return Physics2D.OverlapCircle(rorMarken.position, radieMario, marken); 
    }

    //Vänder Marios riktning till där han går
    private void bytHall()
    {    
        if (hoger && sidleds < 0f || !hoger && sidleds > 0f) //Kollar om Mario rör sig höger medan han kollar vänster och vice versa
        {
            hoger = !hoger; 
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; 
            transform.localScale = localScale;
        }
    }

    //Sparar antalet coins man plockar upp till en textfil
    private void AntalCoins(int antal)
    {   
        string path = "Assets/AntalCoins.txt"; //Vart filen ska vara
        string text = "Antal samlade Coins under ditt senaste försök: " + ch.coinCount.ToString(); //Texten i text filen   
        File.WriteAllText(path, text);  
    }

    // Hanterar vad som händer när Mario nuddar specifika objekt
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Coin")) //Kollar om objektet har taggen "Coin"
        {
            Destroy(other.gameObject); //Tar bort coinen så att man inte kan plocka upp samma coin flera gånger.
            ch.coinCount++;  //Ökar coinCount i coinHanterare scripten med 1 för varje gång man rör ett objekt med taggen "Coin"
        }
        
        if(other.gameObject.CompareTag("Win")) //Kollar om objektet har taggen "Win"
        {    
            Destroy(other.gameObject); //Tar bort trofén efter man "plockat upp" den (nuddat den).
            AntalCoins(ch.coinCount); //Kallar AntalCoins metoden som sparar antalet coins man plockat upp till en text fil
            gameOverScreen.gameOver();
        }
    }
}

