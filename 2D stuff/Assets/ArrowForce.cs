using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowForce : MonoBehaviour {
    Quaternion rotation;
    float speed;
    float v;
    float g;
    public float force;
    public float angle;
    float x;
    float y;
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        //StartingAngle();
    }

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        //LaunchArrow();
    }

    // Update is called once per frame
    void Update () {
        UpdateAngle();
    }

    void StartingAngle()
    {
        /*g = rb.gravityScale;
        v = rb.velocity.magnitude;
        angle = Mathf.Atan((-v * v + Mathf.Sqrt(v * v * (v * v + 2 * y * g) - g * g * x * x)) / (g * x));
        Debug.Log((-v * v + Mathf.Sqrt(v * v * (v * v + 2 * y * g) - g * g * x * x)) / (g * x));
        angle = Mathf.Rad2Deg*angle;*/
        Quaternion rotation = Quaternion.Euler(0, 0, angle);;
        this.transform.rotation *= rotation;
    }

    void LaunchArrow()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle); ;
        this.transform.rotation *= rotation;
        rb.velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * force, Mathf.Sin(Mathf.Deg2Rad * angle) * force, 0);
    }

    void UpdateAngle()
    {
        rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.y, rb.velocity.x));
        this.transform.rotation = rotation;
    }

    public void SetAngle(float angle)
    {
        this.angle = angle;
        //Debug.Log(angle);
        LaunchArrow();
    }
}
