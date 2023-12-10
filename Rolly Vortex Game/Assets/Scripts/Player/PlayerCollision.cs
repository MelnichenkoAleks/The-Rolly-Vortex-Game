using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject score;

    private bool canCollectGem = true;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        score.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            audioManager.Play("Game Over");
            PlayerManager.gameOver = true;
            score.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canCollectGem) return;

        if (other.gameObject.CompareTag("Gem"))
        {
            audioManager.Play("Coin");
            PlayerManager.gems++;
            PlayerManager.score++;
            canCollectGem = false; 
            StartCoroutine(ResetGemCollection()); 
            Destroy(other.gameObject);
        }
    }

    private IEnumerator ResetGemCollection()
    {
        yield return new WaitForSeconds(0.1f);
        canCollectGem = true;
    }
}
