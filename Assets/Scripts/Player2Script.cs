using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2Script : MonoBehaviour
{


    bool jumping1 = false;
    bool jumping2 = false;
    float jumpStrength = 9;
    float distanceFromGround;
    float movementSpeed = 4;
    float initialMovementSpeed;
    float maxRunSpeed = 6;
    LineRenderer line;
    float lengthOfLightPreview = 6;
    Vector2 rightStickDirection;
    bool ableToshootLight = false;
    float despawnLightPoint = 3;                                                    //how far away light has to be from the edge of the screen to despawn, 1 is the edge
    public static float speedOfLight = 150;
    public static float lightDelay = 0;
    float lightTimer = lightDelay;
    int prevLightCount;
    public static List<GameObject> lightList = new List<GameObject>();
    Collider2D col;

    // Use this for initialization
    void Start()
    {
        initialMovementSpeed = movementSpeed;

        line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.sortingLayerName = "Default";




    }



    // Update is called once per frame
    void Update()
    {

        //controls for sprinting
        if (Input.GetButton("LB"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed * Input.GetAxis("LX"), gameObject.GetComponent<Rigidbody2D>().velocity.y);
            if (movementSpeed < maxRunSpeed)
            {
                movementSpeed += .2f;
            }
        }
        else if (gameObject.GetComponent<Rigidbody2D>().velocity.x == 0 || !Input.GetButton("LB"))
        {
            movementSpeed = initialMovementSpeed;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed * Input.GetAxis("LX"), gameObject.GetComponent<Rigidbody2D>().velocity.y);


        //controls for jumping
        if (Input.GetButtonDown("A") && !jumping2)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, jumpStrength);
            if (jumping1)
            {
                jumping2 = true;
            }
            jumping1 = true;

        }

        //controls for shooting light
        rightStickDirection = new Vector2(Input.GetAxis("RX"), Input.GetAxis("RY"));

        if (getMagnitude(rightStickDirection) >= .8f)
        {
            //linerenderer
            line.enabled = true;
            line.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f));
            line.SetPosition(1, new Vector2(transform.position.x, transform.position.y) + setMagnitude(lengthOfLightPreview, rightStickDirection));

            ableToshootLight = true;
        }
        else line.enabled = false;


        if (getMagnitude(rightStickDirection) == 0)
        {
            ableToshootLight = true;
        }

        if (Input.GetAxis("RT") > .9f && getMagnitude(rightStickDirection) > .8f && ableToshootLight)
        {
            rightStickDirection = setMagnitude(1, rightStickDirection);
            GameObject prefab = (GameObject)Resources.Load("Player/Light");
            GameObject light = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
            light.GetComponent<Rigidbody2D>().velocity = setMagnitude(speedOfLight, rightStickDirection);
            lightList.Add(light);
            ableToshootLight = false;
        }






    }


    void FixedUpdate()
    {

        lightTimer += 1;

        Vector2 feet = new Vector2(transform.position.x, transform.position.y - 1.8f);
        RaycastHit2D hit = Physics2D.Raycast(feet, -Vector2.up);
        if (hit.collider != null)
        {
            distanceFromGround = Mathf.Abs(hit.point.y - transform.position.y);
        }

        if (distanceFromGround <= 1.9f && jumping2)
        {
            jumping1 = false;
            jumping2 = false;
        }


    }


    //is called after update
    public void LateUpdate()
    {

    }


    public float getMagnitude(Vector2 vector)
    {
        float magnitude;
        magnitude = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
        return magnitude;
    }

    public Vector2 setMagnitude(float magnitude, Vector2 vector)
    {
        float ratio;
        ratio = Mathf.Sqrt(magnitude * magnitude / (vector.x * vector.x + vector.y * vector.y));
        Vector2 result = new Vector2(vector.x * ratio, vector.y * ratio);
        return result;
    }

    public Vector3 getWorldMouseCoordinates()
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(mouse);
        Vector3 mousePos1 = new Vector3(2f * (mousePos.x - .5f), 2f * (mousePos.y - .5f));
        Vector3 mouseUnit = new Vector3(Camera.main.aspect * Camera.main.orthographicSize * mousePos1.x, Camera.main.orthographicSize * mousePos1.y);
        return mouseUnit;
    }
}
	