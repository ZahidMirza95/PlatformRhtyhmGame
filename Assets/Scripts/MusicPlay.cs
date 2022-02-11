using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] GameObject player;
    bool alreadyPlaying;
    [SerializeField] int startPos;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //Debug.Log(player.GetComponent<Rigidbody2D>().position.x);
        if (player.GetComponent<Rigidbody2D>().position.x >= startPos && !alreadyPlaying)
        {
            audioSource.Play();
            alreadyPlaying = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
