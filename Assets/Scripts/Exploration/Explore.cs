using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Explore : MonoBehaviour
{
    // 탐험 스크립트

    public float BGspeed;
    public float LRspeed;
    public GameObject[] backgrounds;

    float leftPosY = 0f;
    float rightPosY = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;


    void Start()
    {
        BGspeed = 500f;
        LRspeed = 400f;

        xScreenHalfSize = xScreenHalfSize * Camera.main.aspect;
        yScreenHalfSize = Camera.main.orthographicSize;


        leftPosY = -(yScreenHalfSize * 2);
        rightPosY = yScreenHalfSize * 2 * backgrounds.Length;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                //                backgrounds[i].GetComponent<RectTransform>().anchoredPosition += new Vector3(0, -BGspeed, 0) * Time.deltaTime;

                backgrounds[i].GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -BGspeed) * Time.deltaTime;

                if (backgrounds[i].gameObject.transform.position.y < leftPosY)
                {
                    Vector2 nextPos = backgrounds[i].GetComponent<RectTransform>().anchoredPosition;

                    nextPos = new Vector2(nextPos.x, nextPos.y + rightPosY);
                    backgrounds[i].GetComponent<RectTransform>().anchoredPosition = nextPos;
                }
            }
        }

        transform.position += moveVelocity * LRspeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Event")
        {
            GameObject.FindWithTag("EventManager").GetComponent<EventPanel>().OpanEventPanel();
            BGspeed = LRspeed = 0;
            Debug.Log("충돌함");
        }
    }

}
