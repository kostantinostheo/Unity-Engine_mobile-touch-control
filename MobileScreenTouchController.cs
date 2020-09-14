using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileScreenTouchController : MonoBehaviour
{

    bool LeftLine1 = false;
    bool LeftLine2 = true;

    bool RightLine2 = false;
    bool RightLine1 = false;

    bool canClick = true;

    Vector2 targetPos;
    public float Xincrement;
    public float speed;
    int point = 1;

    public AudioSource MoveSound;
    public AudioSource pointSound;


    


    void Start()
    {
        targetPos = transform.position;
    }

    

void Update()
    {
       
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                return;
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch t = Input.GetTouch(0);

                    if (t.phase == TouchPhase.Began)
                    {

                        if (t.position.x < Screen.width / 2 - 100 && canClick)
                        {
                            Left();
                            canClick = false;
                            StartCoroutine(MyDelay(0.1f));
                        }
                        else if (t.position.x > Screen.width / 2 + 100 && canClick)
                        {
                            Right();
                            canClick = false;
                            StartCoroutine(MyDelay(0.1f));
                        }
                    }
                }
            }
            
        }        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left();
            canClick = false;
            StartCoroutine(MyDelay(0.1f));
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right();
            canClick = false;
            StartCoroutine(MyDelay(0.1f));
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        
        



    }

    public void Left()
    {
        goLeft();
    }
    public void Right()
    {
        goRight();
    }

    private void goLeft()
    {
        if (LeftLine2 == true)
        {
            MoveSound.Play();
            targetPos = new Vector2(transform.position.x - 0.88f, transform.position.y);
            LeftLine2 = false;
            LeftLine1 = true;
        }
        else if (RightLine2 == true)
        {
            MoveSound.Play();
            targetPos = new Vector2(transform.position.x - Xincrement, transform.position.y);
            RightLine2 = false;
            LeftLine2 = true;
        }
        else if (RightLine1 == true)
        {
            MoveSound.Play();
            targetPos = new Vector2(transform.position.x - 0.88f, transform.position.y);
            RightLine1 = false;
            RightLine2 = true;
        }
        else
        {
            return;
        }

    }
    private void goRight()
    {
        if (LeftLine1 == true)
        {
            MoveSound.Play();
            targetPos = new Vector2(transform.position.x + 0.88f, transform.position.y);
            LeftLine1 = false;
            LeftLine2 = true;
        }
        else if (LeftLine2 == true)
        {
            MoveSound.Play();
            targetPos = new Vector2(transform.position.x + Xincrement, transform.position.y);
            LeftLine2 = false;
            RightLine2 = true;
        }
        else if (RightLine2 == true)
        {
            MoveSound.Play();
            targetPos = new Vector2(transform.position.x + 0.88f, transform.position.y);
            RightLine2 = false;
            RightLine1 = true;
        }
        else
        {
            return;
        }
    }


    private IEnumerator MyDelay(float sec)
    {
        yield return new WaitForSeconds(sec);
        canClick = true;
    }
}
