using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public float song_bpm;
    public float sec_per_beat;

    public float song_start_time; // seconds since song start
    public float song_pos;        // song position in seconds
    public float curr_beat;       // song position in beats

    public float next_beat;
    public List<float> beat_list;
    public int beat_index = 0;

    public PlayerController player;

    public bool verbose = false;

    // Start is called before the first frame update
    void Start()
    {
        // Check for common errors with Metronome instantiation
        // (these might want to be try/catch)
        if (song_bpm <= 0)
        {
            Debug.LogError("Metronome.song_bpm must be set in inspector to a positive integer.");
        }
        if (player == null)
        {
            Debug.LogError("Metronome.player must be set in inspector to a member or child member of the PlayerController class");
        }
        
        // Calculate beat
        sec_per_beat = 60f / song_bpm;

        // Get beats ready
        float[] default_beats = { 1f, 2f, 3f, 4f, 5f, 5.5f, 6.5f };
        beat_list = new List<float>(default_beats);
        curr_beat = 0;
        next_beat = beat_list[0];

        // Record music start time and go!
        //dsp_song_time = (float) AudioSettings.dspTime;
        song_start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine song position and beats since start of song
        //song_pos = (float)(AudioSettings.dspTime - dsp_song_time);
        song_pos = Time.time - song_start_time;
        curr_beat = song_pos / sec_per_beat;

        // Do BeatAction when beat hits
        if (curr_beat >= next_beat && player.actionable)
        {
            if (verbose) LogOnBeat();
            player.BeatAction();

            beat_index++;
            if (beat_index >= beat_list.Count)
            {
                player.actionable = false;
            }
            else
            {
                next_beat = beat_list[beat_index];
            }
        }
    }

    private void LogTimeUntilBeat()
    {
        Debug.Log("Beat: " + curr_beat + 
            ", Until next beat: " + (next_beat - curr_beat));
    }

    private void LogOnBeat()
    {
        Debug.Log(">> On beat!\n>>Beat: " + curr_beat + 
            ", Target beat: " + next_beat);
    }
}
