using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public Vector2 inputDirection,lookDirection;
    Animator anim;

    Vector3 touchStart,touchEnd;
    public Image dpad;
    public float dpadRadius;

    Touch theTouch;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = 5;
        Time.timeScale = 1;

        //makes the character look down by default
        lookDirection = new Vector2(0, -1);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        calculateDesktopInputs();
#elif UNITY_WEBGL
        calculateMobileInput();
#elif UNITY_IOS
        calculateTouchInput();
#endif

        //sets up the animator
        animationSetup();

        //moves the player
        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);

        if (health <= 0)
        {
            GameOver();
        }
        if (health > 7)
        {
            health = 7;
        }
    }

    void calculateDesktopInputs()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(x, y).normalized;
    }

    void animationSetup()
    {
        //checking if the player wants to move the character or not
        if (inputDirection.magnitude > 0.1f)
        {
            //changes look direction only when the player is moving, so that we remember the last direction the player was moving in
            lookDirection = inputDirection;

            //sets "isWalking" true. this triggers the walking blend tree
            anim.SetBool("isWalking", true);
        }
        else
        {
            // sets "isWalking" false. this triggers the idle blend tree
            anim.SetBool("isWalking", false);

        }

        //sets the values for input and lookdirection. this determines what animation to play in a blend tree
        anim.SetFloat("inputX", lookDirection.x);
        anim.SetFloat("inputY", lookDirection.y);
        anim.SetFloat("lookX", lookDirection.x);
        anim.SetFloat("lookY", lookDirection.y);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
    }

    void calculateMobileInput()
    {
        //gets left mb
        if (Input.GetMouseButton(0))
        {
            dpad.gameObject.SetActive(true);

            //the mouse position where the click started is recorded
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Input.mousePosition;
            }

            //the mouse position while the button is held down is recorded
            touchEnd = Input.mousePosition;

            //difference between start and current position is calculated
            float x = touchEnd.x - touchStart.x;
            float y = touchEnd.y - touchStart.y;

            //sets the input direction
            inputDirection = new Vector2(x,y).normalized;

            if ((touchEnd - touchStart).magnitude > dpadRadius)
            {
                dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
            }
            else
            {
                dpad.transform.position = touchEnd;
            }
        }
        else
        {
            inputDirection = Vector2.zero;
            dpad.gameObject.SetActive(false); 
        }
    }
    void calculateTouchInput()
    {
        if (Input.touchCount > 0)
        {
            dpad.gameObject.SetActive(true);
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchStart = theTouch.position;
            }
            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEnd = theTouch.position;

                float x = touchEnd.x - touchStart.x;
                float y = touchEnd.y - touchStart.y;

                //sets the input direction
                inputDirection = new Vector2(x, y).normalized;

                if ((touchEnd - touchStart).magnitude > dpadRadius)
                {
                    dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
                }
                else
                {
                    dpad.transform.position = touchEnd;
                }
            }
        }
        else
        {
            inputDirection = Vector2.zero;
            dpad.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "RedBall")
        {
            health--;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "GreenBall")
        {
            health += 2;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "BlueBall")
        {
            moveSpeed *= 1.05f;
            Destroy(collision.gameObject);
        }
    }
}
