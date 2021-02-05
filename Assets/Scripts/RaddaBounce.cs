using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaddaBounce : MonoBehaviour
{
    // Member variables
    public float min_velocity = 0.01f;
    public float max_velocity = 0.15f;

    public float vel_x = 0.01f;
    public float vel_y = 0.01f;
    public float acceleration = 1.01f;

    // Start is called before the first frame update
    void Start()
    {
        // Randomize starting position within current bounds of camera
        Vector3 upper_bound = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 lower_bound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        transform.position = new Vector3(Random.Range(lower_bound.x, upper_bound.x), 
            Random.Range(lower_bound.y, upper_bound.y));
    }

    // Update is called once per frame
    void Update()
    {
        // This should probably be accomplished with RigidBody2Ds, not updating pos, but I'll figure that out later
        Vector3 upper_bound = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 lower_bound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 pos = transform.position;

        if (pos.x >= upper_bound.x || pos.x <= lower_bound.x)
        {
            vel_x *= -1;
        }
        if (pos.y >= upper_bound.y || pos.y <= lower_bound.y)
        {
            vel_y *= -1;
        }

        transform.position += new Vector3(vel_x * Time.deltaTime, vel_y * Time.deltaTime);
    }
}
