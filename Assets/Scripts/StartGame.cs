using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Go()
    {
        PlayerPrefs.DeleteAll();
        int lvlNumber = PlayerPrefs.GetInt("lvl", 0);

        string lvl = "lvl" + lvlNumber;
        SceneManager.LoadScene(lvl);
    }
}
