using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool gotten = false;

    private static GameMaster instance;
    public Vector2 lastCheckPointPos;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        else
        {
             Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (gotten)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Hook>().enabled = true;
        }
    }
}
