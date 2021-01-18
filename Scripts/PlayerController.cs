using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    // because speed is public i can see it on the unity menu
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject looseTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private Vector3 startPos;

    //added to try and use accelerometer
    void Main()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    /*
    void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (Input.GetKey("escape"))
                Application.Quit();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        looseTextObject.SetActive(false);
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
        }
    }

    void FixedUpdate()
    {
        /*
        if (rb = null)
        {
            Debug.Log("Failed to get rigidbody component from gameobject.");
        }
        else
            Debug.Log("Rigidbody component from gameobject is aquired.");
        */
        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //rb.AddForce(movement * speed);
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
        else
        {
            Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
            /*
            //Vector3 acclerometerValue = rawAccelValue();
            //Debug.Log("X: " + acclerometerValue.x + "Y: " + acclerometerValue.y + " Z: " + acclerometerValue.z);
            //rb.AddForce(acclerometerValue * speed);
            */
            rb.AddForce(movement * speed);
        }

    }
    /*
    Vector3 rawAccelValue()
    {
        return Input.acceleration;
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count+1;
            //each time u hit a ball the count goes up by 1
            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy")){
            this.transform.position=startPos;
            count = count -1;
            if (count<0){
                looseTextObject.SetActive(true);
                //easy way to stop the game while i work on a reset button
                speed=0;
                movementX=0;
                movementY=0;
            }
            SetCountText();
        }
    }
}
