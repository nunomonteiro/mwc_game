using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // The default Position
    Vector2 startPos;
    public float force = 0.01f;

    public GameObject Arrow;
    private Animator _anim;

    bool _isInGame;
    private bool _isColliding;

    // Use this for initialization
    void Start()
    {
        _isColliding = false;
        startPos = transform.position;
        _anim = GameObject.FindGameObjectWithTag("Arm").GetComponent<Animator>();
        _isInGame = true;
    }

    void OnMouseUp()
    {
        _anim.SetTrigger("shoot");
        GetComponent<Rigidbody2D>().isKinematic = false;
        Vector2 dir = startPos - (Vector2)transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir * force);
        Arrow.SetActive(false);
        _isInGame = false;
    }

    Vector2 difference = Vector2.zero;
    float distance;
    bool pressed,shoot;
    private void Update()
    {



        if (Input.GetMouseButtonDown(0))
        {
            pressed = true;


        }
        if (Input.GetMouseButtonUp(0))
        {
            pressed = false;
            shoot = true;
        }

        if (pressed)
        {
            Arrow.SetActive(true);
            Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            difference = (p - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;
            distance = Vector2.Distance(p, gameObject.transform.position);
            activateDistancePointers();
            if (difference.x > 0 && difference.y > 0)
            {
                float angleRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Arrow.transform.rotation = Quaternion.Euler(0f, 0f, angleRotation-45);
            }
            else
            {
                Arrow.SetActive(false);
            }
        }

        if (shoot)
        {
            _anim.SetTrigger("shoot");
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().AddForce(difference * _forceCalculator(difference));
            Arrow.SetActive(false);
            _isInGame = false;
            shoot = false;
        }
    }


    float _forceCalculator(Vector2 dir)
    {
        print(distance);
        if (distance > 10 )
        {
            return 1400;
        }
        else
        {
            return (1400 * distance) / 10;
        }
    }

    void activateDistancePointers()
    {
        if (distance >= 9)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(true);
            Arrow.transform.GetChild(2).gameObject.SetActive(true);
            Arrow.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (distance < 9 && distance >= 6)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(true);
            Arrow.transform.GetChild(2).gameObject.SetActive(true);
            Arrow.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (distance < 6 && distance >= 3)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(true);
            Arrow.transform.GetChild(2).gameObject.SetActive(false);
            Arrow.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if(distance < 3)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(false);
            Arrow.transform.GetChild(2).gameObject.SetActive(false);
            Arrow.transform.GetChild(3).gameObject.SetActive(false);
        }
    }


    /*
    void OnMouseDrag()
    {
        if (_isInGame)
        {


            // Keep it in a certain radius
            float radius = 0.5f;
            Vector2 dir = p - startPos;
            if (dir.sqrMagnitude > radius)
                dir = dir.normalized * radius;

            // Set the Position
            transform.position = startPos + dir;



            Vector2 difference = (p - new Vector2(Arrow.transform.position.x, Arrow.transform.position.y)).normalized;
            float angleRotation = Mathf.Atan2(difference.y, difference.x)* Mathf.Rad2Deg;

            print("angleRotation: " +angleRotation);


            Arrow.transform.rotation = Quaternion.Euler(0f,0f, angleRotation-90);

        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
        {
        if (collision.CompareTag("Ring"))
        //Debug.Log("============================================== Fiz trigger ==============================================");
            GameManager.Instance.WentThroughRing(collision.gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (_isColliding)
            return;

        if (coll.collider.gameObject.tag == "Obstacle")
            GameManager.Instance.OnTouchedBarrier(coll.collider.gameObject);

        _isColliding = true;

        if (!IsInvoking("NotifyEndTurn")) {
            GameManager.Instance.LostAttempt();
            Invoke("NotifyEndTurn", 1.5f);
        }
    }

    void NotifyEndTurn()
    {
        Destroy(this.gameObject);
        GameManager.Instance.EndTurn();
    }
}
