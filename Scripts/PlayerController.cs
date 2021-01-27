using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // because speed is public i can see it on the unity menu
    public float speed = 1;
    // To display and associate the "UI" 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject looseTextObject;
    public GameObject restartButton;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private Vector3 startPos;

    void Main()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        //Hiding Win/Loose text and Reset Button.
        winTextObject.SetActive(false);
        looseTextObject.SetActive(false);
        restartButton.SetActive(false);
        //Player start position.
        startPos = this.transform.position;        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        //each time this function is called it will store the desired text on the countText variable
        if (count >= 12)
        {
            winTextObject.SetActive(true);
            restartButton.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        //this if call was made to use same script for accelerometer or keyboard but aint working, will leave it there anyways
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
        else
        {   
            Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
            rb.AddForce(movement * speed);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Player picking up good items
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count+1;
            //each time u hit a ball the count goes up by 1
            SetCountText();
        }
        //Enemy hitting the player
        if (other.gameObject.CompareTag("Enemy")){
            //Player goes to start position and looses points.
            this.transform.position=startPos;
            count = count -1;
            if (count<0){
                //When player is out of points cant move and a Reset button appears on screen.
                looseTextObject.SetActive(true);
                speed=0;
                movementX=0;
                movementY=0;
                restartButton.SetActive(true);
            }
            SetCountText();
        }
    }
}
