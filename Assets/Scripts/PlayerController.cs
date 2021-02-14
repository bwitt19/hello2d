using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public bool actionable;
    public KeyCode action_key;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Player should be actionable at start
        actionable = true;
    }

    // Update is called once per frame
    public virtual void Update() 
    { 
    
    }

    public abstract void BeatAction();
}
