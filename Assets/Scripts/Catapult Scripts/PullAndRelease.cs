using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullAndRelease : MonoBehaviour {

    // The default Position
    Vector2 startPos;
    float radius;
    public float force = 0.01f;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        anim = GameObject.FindGameObjectWithTag("Arm").GetComponent<Animator>();    
    }

    void OnMouseUp()
    {
        anim.SetTrigger("shoot");
        GetComponent<Rigidbody2D>().isKinematic = false;
        Vector2 dir = startPos - (Vector2)transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir * force);
        Destroy(this);
    }




    void OnMouseDrag()
    {
        // Convert mouse position to world position
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Keep it in a certain radius
        float radius = 0.5f;
        Vector2 dir = p - startPos;
        if (dir.sqrMagnitude > radius)
            dir = dir.normalized * radius;

        // Set the Position
        transform.position = startPos + dir;
    }
}
