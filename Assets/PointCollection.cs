using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointCollection : MonoBehaviour
{
    int points;
    Rigidbody rb;

    Vector3 defaultScale;
    Vector3 maxScale;
    bool canChangeSize = true;
    private GameObject parent;
    private GameObject player;
    int pointCountInScene;
    int sceneCount;

    // Use this for initialization  
    void Start()
    {     
       
        if ((SceneManager.GetActiveScene().name != "Level1") && (PlayerPrefs.GetInt("currentLevel") != 0))
        { 
            
            sceneCount = PlayerPrefs.GetInt("currentLevel");    
        }
        else
        {
            PlayerPrefs.DeleteKey("currentLevel");
            sceneCount = 1;
            PlayerPrefs.SetInt("currentLevel", sceneCount);
        }

        parent = GameObject.Find("playerparent");
        player = GameObject.Find("PlayerPrefab");
        defaultScale = parent.transform.localScale;
        maxScale.y = (defaultScale.y * 2);
        maxScale.x = (defaultScale.y * 2);
        maxScale.z = (defaultScale.y * 2);
        points = 0;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Delay());
        var sludgeCount = GameObject.FindGameObjectsWithTag("point_score");
        pointCountInScene = sludgeCount.Length;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds((float)1.2);
        canChangeSize = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "point_score")
        {
            points += 1;
            Destroy(other.gameObject);
            var position = player.transform.position;
            parent.transform.localScale += new Vector3(0.15F, 0.15F, 0.15F);
            player.transform.position = position;
            if ((parent.transform.localScale.x > maxScale.x))
            {
                parent.transform.localScale = maxScale;
                player.transform.position = position;
            }
            print("Points " + points.ToString() + "of possible" + pointCountInScene.ToString());
        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            if (canChangeSize)
            {
                print("sizeChanged");
                var position = player.transform.position;
                parent.transform.localScale += new Vector3(-0.15F, -0.15F, -0.15F);
                player.transform.position = position;
                if ((parent.transform.localScale.x < defaultScale.x))
                {
                    parent.transform.localScale = defaultScale;
                    player.transform.position = position;
                    FailScreen();
                }
                canChangeSize = false;
                StartCoroutine(Delay());
            }
            else { print("sizeNotChanged"); }
        }
        if (collision.gameObject.tag == "finishLevel")  
        {

            sceneCount += 1;  
            PlayerPrefs.SetInt("currentLevel", sceneCount);
            if(sceneCount == 4)
            {
                SceneManager.LoadScene("KillScreen");
            }
            else
            {
                SceneManager.LoadScene("Level" + sceneCount.ToString());
            }
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        //parent.transform.localPosition = rb.transform.localPosition;
    }

    void FailScreen()
    {
       SceneManager.LoadScene("KillScreen");
    }

}