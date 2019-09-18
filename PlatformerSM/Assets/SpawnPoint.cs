using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private Sprite enabled_;

    [SerializeField]
    private Sprite disabled_;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<CharacterAbstract>().SpawnPosition = transform.position;
            SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
            foreach(SpawnPoint spawnPoint in spawnPoints)
            {
                spawnPoint.GetComponent<SpriteRenderer>().sprite = disabled_;
            }
            GetComponent<SpriteRenderer>().sprite = enabled_;
        
        }
    }
}
