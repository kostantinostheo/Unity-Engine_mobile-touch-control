using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileScreenTouchController : MonoBehaviour
{
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
        /*
        Uncomment this to debug using keyboard arrows.
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
        }*/
        
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
    //Move Player Left & Right by Xincrement points.
    private void goLeft()
    {
        MoveSound.Play();
        targetPos = new Vector2(transform.position.x - Xincrement, transform.position.y);
    }
    private void goRight()
    {
         MoveSound.Play();
         targetPos = new Vector2(transform.position.x + Xincrement, transform.position.y);
    }
        
    //Creates a small delay between touches.
    private IEnumerator MyDelay(float sec)
    {
        yield return new WaitForSeconds(sec);
        canClick = true;
    }
}
