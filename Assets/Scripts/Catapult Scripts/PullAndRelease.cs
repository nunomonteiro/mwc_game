using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullAndRelease : MonoBehaviour {

    // The default Position
    Vector2 startPos;
    float radius;
    public float force = 0.01f;

    private Animator _anim;

    bool _isInGame;
    private bool _isColliding;

    //TODO
    GameManager _game;

    // Use this for initialization
    void Start()
    {
        _isColliding = false;
        startPos = transform.position;
        _anim = GameObject.FindGameObjectWithTag("Arm").GetComponent<Animator>();
        _isInGame = true;
        _game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void OnMouseUp()
    {
        _anim.SetTrigger("shoot");
        GetComponent<Rigidbody2D>().isKinematic = false;
        Vector2 dir = startPos - (Vector2)transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir * force);

        _isInGame = false;
    }




    void OnMouseDrag()
    {
        if (_isInGame)
        {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("============================================== Fiz trigger ==============================================");
        _game.WentThroughRing(collision.gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (_isColliding)
            return;

        _isColliding = true;
        Invoke("_endGame",2);
    }

    //TODO
    public void _endGame()
    {
        _game.EndGame();
    }
}
