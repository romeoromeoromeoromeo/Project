using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinHanterare : MonoBehaviour
{
    public int coinCount; //Public då den används utanför denna klass.
    public TMPro.TextMeshProUGUI coinText; //Tar Text Mesh Pro texten, public då den används i Unity

    // Update is called once per frame
    void Update()
    {
        coinText.text = coinCount.ToString(); //Ändrar den till coinCount som ändras i rörelse.cs
    }
}


