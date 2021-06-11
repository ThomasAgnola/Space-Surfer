using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float strafeSpeed = 7.5f, hoverSpeed = 5f, forwardspeed = 25f;
    private float activeStrafeSpeed, activeHoverSpeed, activeForwardSpeed;
    public float speed = 1.0f;
    Transform target = null;
    Transform Ship_straight;
    private bool IsLeft=false, IsRight=false, IsCenter=true, IsMoving=false;

    [SerializeField] GameObject Left_Ship_Position;
    private Transform ShipLeft;
    [SerializeField] GameObject Base_Ship_Position;
    private Transform ShipBase;
    [SerializeField] GameObject Right_Ship_Position;
    private Transform ShipRight;

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
        if (Input.GetAxisRaw("Horizontal") > 0) // Vers la droite
        {
            //Debug.Log("Horizontal > 0");
            if (IsCenter)
            {
                IsRight = true;
                Movement();
                Debug.Log("Déplacement à droite init");
            }
            if (IsLeft)
            {
                IsCenter = true;
                Movement();
            }
        }
        if (Input.GetAxisRaw("Horizontal") < 0) // Vers la gauche
        {
            if (IsCenter)
            {
                IsLeft = true;
                Movement();
                Debug.Log("Déplacement à gauche init");
            }
            if(IsRight)
            {
                IsCenter = true;
                Movement();
            }
        }
        /*if (IsMoving)
        {
            Movement();
        }*/
    }

    void Movement()
    {
        Debug.Log("Déplacement");
        if(IsRight)
        {
            target = Right_Ship_Position.transform;
            IsMoving = true;
            Ship_straight = transform;
            transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z + 30, transform.rotation.w);
        }
        if (IsLeft)
        {
            target = Left_Ship_Position.transform;
            IsMoving = true;
            Ship_straight = transform;
            transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z - 30, transform.rotation.w);
        }
        if (IsCenter)
        {
            target = Base_Ship_Position.transform;
            IsMoving = true;
        }
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            // Swap the position of the cylinder.
            Debug.Log("Distance : " + Vector3.Distance(transform.position, target.position)+ "\n");
            target.position *= -1.0f;
            transform.rotation.Set(Ship_straight.rotation.x, Ship_straight.rotation.y, Ship_straight.rotation.z, Ship_straight.rotation.w);
            IsMoving = false;
            target = null;
        }
    }
}