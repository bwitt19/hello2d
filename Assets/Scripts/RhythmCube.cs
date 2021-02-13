using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmCube : MonoBehaviour
{
    public float song_bpm;
    public float sec_per_beat;

    public float song_start_time; // seconds since song start
    public float song_pos;        // song position in seconds
    public float curr_beat;       // song position in beats

    public float next_beat;
    public List<float> beat_list;
    public int beat_index = 0;
    
    public AudioSource up_beat;
    public AudioSource down_beat;
    public bool actionable;

    // Start is called before the first frame update
    void Start()
    {
        // Set starting color of object
        gameObject.GetComponent<Renderer>().material.color = Color.gray;

        // Calculate beat
        sec_per_beat = 60f / song_bpm;

        // Get beats ready
        float[] default_beats = { 1f, 2f, 3f, 4f, 5f, 5.5f, 6.5f };
        beat_list = new List<float>(default_beats);
        curr_beat = 0;
        next_beat = beat_list[0];

        actionable = true;

        // Record music start time and go!
        //dsp_song_time = (float) AudioSettings.dspTime;
        song_start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine how many seconds since the song started
        //song_pos = (float)(AudioSettings.dspTime - dsp_song_time);
        song_pos = Time.time - song_start_time;

        // Determine how many beats since the song started
        curr_beat = song_pos / sec_per_beat;

        // Do BeatAction when beat hits
        if (curr_beat >= next_beat && actionable)
        {
            Debug.Log(">> On beat!\n>>Beat: " + curr_beat + 
                ", Target beat: " + next_beat );
            BeatAction();

            beat_index++;
            if (beat_index >= beat_list.Count)
            {
                actionable = false;
            }
            else
            {
                next_beat = beat_list[beat_index];
            }
        }

        //Debug.Log("Beat: " + curr_beat + 
        //    ", Until next beat: " + (next_beat - curr_beat));
    }

    public void BeatAction()
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
