using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    public GameObject gameOverUI; //Public då den måste länkas i Unity

    //Metod för att visa game over skärmen, public då den används utanför denna klass
    public void gameOver()
    {
        gameOverUI.SetActive(true); 
    }

    //Metod som startar om spelet, public då den används utanför denna klass
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
