using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillScreenScript : MonoBehaviour {

    private bool doChangeText;
    private GameObject theText;  
    int textCounter;

	// Use this for initialization
	void Start () {
        textCounter = 0;
        doChangeText = false;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()  
    {
        yield return new WaitForSeconds((float)3);
        textCounter += 1;
        doChangeText = true;  
    }

    // Update is called once per frame 
    void Update() {

       /* if ((textCounter > 1) && (textCounter < 3))
        {
            string s = "";
            TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
            for (int i = 0; i < PlayerPrefs.GetInt("levels"); i++)
            {
                if (PlayerPrefs.HasKey(i.ToString())) {
                    s = s + "In level" + i.ToString() + " you got " + PlayerPrefs.GetString(i.ToString()) + ';';
                } else
                {

                }
            }
            t.text = s;

            PlayerPrefs.DeleteAll();  
        } */
        if (doChangeText)  
        {
            TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
            t.text = "Press R to Restart\n " +
                "     and Q to quit!";

            PlayerPrefs.DeleteAll();
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("Level1");      
            }
            else if (Input.GetKey(KeyCode.Q)) 
                Application.Quit();  

        }
        else
        {
            if (PlayerPrefs.GetInt("currentLevel") == 4)
            {
                TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
                t.text = "Congratulations!!\n " +
                         "you finished the game!";

            }
            PlayerPrefs.SetInt("currentLevel", 0);
        }

    }
}
