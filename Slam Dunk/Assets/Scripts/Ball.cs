using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private AudioSource ballSound;

    private void OnTriggerEnter(Collider other)
    {
        ballSound.Play();

        if(other.CompareTag("Basket"))
        {
            _GameManager.Basket(transform.position);
        }
        else if(other.CompareTag("GameOver"))
        {
            _GameManager.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ballSound.Play();
    }
}
