using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HookGet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<GameMaster>().gotten = true;

            FindObjectOfType<MusicManager>()?.PlayIntenseMusic();
        }
    }
}
