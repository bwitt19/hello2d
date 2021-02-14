using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmCube : PlayerController
{
    public AudioSource up_beat;
    public AudioSource down_beat;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        // Set starting color of object
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }

    // Update is called once per frame
    //void Update() {}

    public override void BeatAction()
    {
        // I should definitely check this against some bool keeping track of beat instead of color
        Color curr_color = gameObject.GetComponent<Renderer>().material.color;
        if (curr_color != Color.green)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            transform.position += new Vector3(0, -0.1f);
            up_beat.Play();
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            transform.position -= new Vector3(0, -0.1f);
            down_beat.Play();
        }
    }

    
}
