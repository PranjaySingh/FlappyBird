using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;

    public float gravity = -9.8f;
    public float strength = 5f;

    public GameObject gameManager;

    AudioSource[] audio;

    public AudioClip score;
    public AudioClip fly;
    public AudioClip die;

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;

        direction = Vector3.zero;

        audio = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
            audio[0].Play();
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        //Debug.Log(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            gameManager.GetComponent<GameManager>().GameOver();
            audio[1].clip = die;
            audio[1].Play();
        }

        else if(other.gameObject.tag == "Scoring")
        {
            gameManager.GetComponent<GameManager>().IncreaseScore();
            audio[1].clip = score;
            audio[1].Play();
        }
    }
}
