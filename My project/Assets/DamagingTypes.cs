using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingTypes : MonoBehaviour
{
    public gameOverScript gameOverScreen;  //Public då den måste länkas i Unity

    //Metod som aktiverar game over skärmenm 
    private void nuddarDamagingType(GameObject player) 
    {
        gameOverScreen.gameOver(); //Kallar gameOver metoden i gameOverScreen klassen
    }
    
    // Metod för att aktivera game over skärmen när man rör en DamagingType
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spelare")) //Kollar om ett objekt har taggen spelare
        {
            nuddarDamagingType(other.gameObject); //Om ja, kalla nuddarDamagingType metoden som kallar gameOver metoden i gameOverScreen klassen som visar game over skärmen
        }
    }
}
