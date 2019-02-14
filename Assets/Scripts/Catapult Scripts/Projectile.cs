using UnityEngine;

public class Projectile : MonoBehaviour {

    // The default Position
    Vector2 startPos;

    public GameObject Arrow;
    public float MiminimumAngle;
    public float MaxAngle;
    private Animator _anim;

    private bool _isColliding;

    Vector2 difference = Vector2.zero;
    float distance;
    bool pressed, shoot, firstpoint;
    public Vector2 inputPoint;
    public Vector2 lastPoint;

    // Use this for initialization
    void Start()
    {
        Arrow.SetActive(false);
        _isColliding = false;
        startPos = transform.position;
        _anim = GameObject.FindGameObjectWithTag("Arm").GetComponent<Animator>();
    }

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
            _pointToTrajectory();
        }

        if (shoot)
        {
            _shootApp();
        }
    }

    private void _pointToTrajectory()
    {
            Arrow.SetActive(true);

            //Get the first inputPoint
            Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!firstpoint)
            {
                firstpoint = true;
                inputPoint = p;
            }

            //Calculate the vector
            difference = (p - new Vector2(inputPoint.x, inputPoint.y)).normalized;

            difference = new Vector2(Mathf.Abs(difference.x), Mathf.Abs(difference.y));

            _activateDistancePointers();

            // Calculate the angle of the input
            float angleRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;


            //Guard to check to not allow the user for strange angles.
            if (angleRotation > MiminimumAngle && angleRotation < MaxAngle)
            {
                Arrow.transform.rotation = Quaternion.Euler(0f, 0f, angleRotation - 45);
            }
            else
            {
                if (angleRotation <= MiminimumAngle)
                {
                    Arrow.transform.rotation = Quaternion.Euler(0f, 0f, MiminimumAngle - 45);
                }
                else
                {
                    Arrow.transform.rotation = Quaternion.Euler(0f, 0f, MaxAngle - 45);
                }
            }
            lastPoint = p;

    }

    private void _shootApp()
    {
        _anim.SetTrigger("shoot");
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(difference * _forceCalculator(difference));
        Arrow.SetActive(false);
        shoot = false;
        firstpoint = false;
        inputPoint = Vector2.zero;
    }


    float _forceCalculator(Vector2 dir)
    {
        if (distance > 10 )
        {
            return 1400;
        }
        else
        {
            return (1400 * distance) / 10;
        }
    }

    void _activateDistancePointers()
    {
        distance = Vector2.Distance(inputPoint, lastPoint);


        if (distance >= 9)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(true);
            Arrow.transform.GetChild(2).gameObject.SetActive(true);
            Arrow.transform.GetChild(3).gameObject.SetActive(true);
            Arrow.transform.GetChild(4).gameObject.SetActive(true);
            Arrow.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if (distance < 9 && distance >= 7)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(true);
            Arrow.transform.GetChild(2).gameObject.SetActive(true);
            Arrow.transform.GetChild(3).gameObject.SetActive(true);
            Arrow.transform.GetChild(4).gameObject.SetActive(true);
            Arrow.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (distance < 7 && distance >= 5)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(true);
            Arrow.transform.GetChild(2).gameObject.SetActive(true);
            Arrow.transform.GetChild(3).gameObject.SetActive(true);
            Arrow.transform.GetChild(4).gameObject.SetActive(false);
            Arrow.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (distance < 4 && distance >= 3)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(true);
            Arrow.transform.GetChild(2).gameObject.SetActive(true);
            Arrow.transform.GetChild(3).gameObject.SetActive(false);
            Arrow.transform.GetChild(4).gameObject.SetActive(false);
            Arrow.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (distance < 3 && distance >= 2)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(true);
            Arrow.transform.GetChild(2).gameObject.SetActive(false);
            Arrow.transform.GetChild(3).gameObject.SetActive(false);
            Arrow.transform.GetChild(4).gameObject.SetActive(false);
            Arrow.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (distance < 2 && distance >= 1)
        {
            Arrow.transform.GetChild(0).gameObject.SetActive(true);
            Arrow.transform.GetChild(1).gameObject.SetActive(false);
            Arrow.transform.GetChild(2).gameObject.SetActive(false);
            Arrow.transform.GetChild(3).gameObject.SetActive(false);
            Arrow.transform.GetChild(4).gameObject.SetActive(false);
            Arrow.transform.GetChild(5).gameObject.SetActive(false);
        }

    }

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
