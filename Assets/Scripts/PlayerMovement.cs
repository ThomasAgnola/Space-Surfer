using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float strafeSpeed = 7.5f, hoverSpeed = 5f, forwardspeed = 25f;
    private float activeStrafeSpeed, activeHoverSpeed, activeForwardSpeed;
    [SerializeField] private float speed = 1;
    Transform Ship_straight;
    private bool IsLeft=false, IsRight=false, IsCenter=true, IsMoving=false;

    [SerializeField] GameObject Left_Ship_Position;
    [SerializeField] GameObject Base_Ship_Position;
    [SerializeField] GameObject Right_Ship_Position;
    private Vector3 target;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*activeForwardSpeed = Input.GetAxisRaw("Vertical") * forwardspeed;
        activeStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        activeHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;

        transform.position += (transform.forward * activeForwardSpeed * Time.deltaTime);
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);
        */

        if (IsMoving)
        {
            Movement();
        }
        else if (Input.GetAxisRaw("Horizontal") > 0) // Vers la droite
        {
            //Debug.Log("Horizontal > 0");
            if (IsCenter)
            {
                IsRight = true;
                IsCenter = false;
                Movement();
                Debug.Log("Déplacement à droite init");
            }
            if (IsLeft)
            {
                IsCenter = true;
                IsLeft = false;
                Movement();
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) // Vers la gauche
        {
            if (IsCenter)
            {
                IsLeft = true;
                IsCenter = false;
                Movement();
                Debug.Log("Déplacement à gauche init");
            }
            if(IsRight)
            {
                IsCenter = true;
                IsRight = false;
                Movement();
            }
        }
    }

    void Movement()
    {
        Debug.Log("Déplacement");
        if(IsRight)
        {
            target = Right_Ship_Position.transform.localPosition; // should return gameobject.x pos but return the player.x pos
            IsMoving = true;
            Ship_straight = transform;
            transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z + 30, transform.rotation.w);
        }
        if (IsLeft)
        {
            target = Left_Ship_Position.transform.localPosition; //same
            IsMoving = true;
            Ship_straight = transform;
            transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z - 30, transform.rotation.w);
        }
        if (IsCenter)
        {
            target = Base_Ship_Position.transform.localPosition; //same
            IsMoving = true;
        }
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, step);
        CheckDistance();

        // Check if the position is approximately equal to the target.
        if (distance <= 0.01f)
        {
            // Swap the position of the cylinder.
            target *= -1.0f;
            transform.rotation.Set(Ship_straight.rotation.x, Ship_straight.rotation.y, Ship_straight.rotation.z, Ship_straight.rotation.w);
            IsMoving = false;
            target = transform.localPosition;
        }
    }

    void CheckDistance()
    {
        distance = Vector3.Distance(transform.localPosition, target);
        //float ma_distance = transform.localPosition.x - target.x;
        Debug.Log("ship x : " + transform.localPosition.x + " target x : " + target.x);
        //Debug.Log("Distance : " + distance + " Ma distance : "+ma_distance);
    }
}