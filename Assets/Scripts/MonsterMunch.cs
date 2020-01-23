using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMunch : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    ParticleSystem particles;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        particles = gameObject.GetComponent<ParticleSystem>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.tag == "Player")
        {
            animator.SetBool("Inside", true);
            col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            col.gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, col.gameObject.transform.position.z);
            col.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            audioSource.Play();
            particles.Play();
            Invoke("NextLevel", 2);
        }
    }

    void NextLevel()
    {
        string currenrtSceneName = SceneManager.GetActiveScene().name;
        int lvlNumber = int.Parse(currenrtSceneName.Substring(3, currenrtSceneName.Length - 3));
        lvlNumber++;
        PlayerPrefs.SetInt("lvl", lvlNumber);
        string nextLvl = "lvl" + lvlNumber;
        SceneManager.LoadScene(nextLvl);
    }
}
