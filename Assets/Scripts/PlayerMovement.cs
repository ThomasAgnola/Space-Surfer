using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class PlayerMovement : MonoBehaviour, IEventHandler
{
    public float strafeSpeed = 7.5f, hoverSpeed = 5f, forwardspeed = 25f;
    private float activeStrafeSpeed, activeHoverSpeed, activeForwardSpeed;
    [SerializeField] private float speed = 1;
    Transform Ship_straight;
    private bool IsLeft = false, IsRight = false, IsCenter = true, IsMoving = false;
    private bool BeforeLeft = false, BeforeRight = false, RotationDone=false;

    [SerializeField] GameObject Left_Ship_Position;
    [SerializeField] GameObject Base_Ship_Position;
    [SerializeField] GameObject Right_Ship_Position;
    private Vector3 target;
    private float distance;
    private float i = 0;


    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<LevelHasBeenInitializedEvent>(LevelHasBeenInitialized);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<LevelHasBeenInitializedEvent>(LevelHasBeenInitialized);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #region Events callbacks
    void GamePlay(GamePlayEvent e)
    {
        Debug.Log("event GamePlay received by + " + name);
    }

    void LevelHasBeenInitialized(LevelHasBeenInitializedEvent e)
    {
        transform.position = e.ePlayerSpawnPos;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
        activeForwardSpeed = Input.GetAxisRaw("Vertical") * forwardspeed;
        activeStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        activeHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;

        transform.position += (transform.forward * activeForwardSpeed * Time.deltaTime);
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

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
                BeforeLeft = true;
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
            if (IsRight)
            {
                IsCenter = true;
                IsRight = false;
                BeforeRight = true;
                Movement();
            }
        }*/
    }

    private void FixedUpdate()
    {
        {
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
                    BeforeLeft = true;
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
                if (IsRight)
                {
                    IsCenter = true;
                    IsRight = false;
                    BeforeRight = true;
                    Movement();
                }
            }
        }
    }

    void Movement()
    {
        Debug.Log("Déplacement");
        if (IsRight)
        {
            target = Right_Ship_Position.transform.localPosition; // should return gameobject.x pos but return the player.x pos
            IsMoving = true;
        }
        if (IsLeft)
        {
            target = Left_Ship_Position.transform.localPosition; //same
            IsMoving = true;
        }
        if (IsCenter)
        {
            target = Base_Ship_Position.transform.localPosition; //same
            IsMoving = true;
        }
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, step);
        CheckDistance();
        if (i < 15 && RotationDone == false)
        {
            rotateship();
            i += 1;
            Debug.Log("rotation deg : " + i);
        }
        else if (i >= 15)
        {
            rotateship();
            i -= 1;
            RotationDone = true;
            Debug.Log("RotationDone est passé à true");
        }
        else if (i > 0 && RotationDone == true)
        {
            rotateship();
            i -= 1;
            Debug.Log("rotation deg : " + i);
        }
        // Check if the position is approximately equal to the target.
        if (distance <= 0.01f)
        {
            // Swap the position of the cylinder.
            target *= -1.0f;
            transform.localEulerAngles = new Vector3(Ship_straight.rotation.x, Ship_straight.rotation.y, Ship_straight.rotation.z);
            IsMoving = RotationDone = BeforeRight = BeforeLeft = false;
            target = transform.localPosition;
            i = 0;
        }
    }

    void CheckDistance()
    {
        distance = Vector3.Distance(transform.localPosition, target);
        //float ma_distance = transform.localPosition.x - target.x;
        //Debug.Log("ship x : " + transform.localPosition.x + " target x : " + target.x);
        //Debug.Log("Distance : " + distance + " Ma distance : "+ma_distance);
    }

    void rotateship()
    {
        if (IsRight)
        {
            Ship_straight = transform;
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - i);
        }
        if (IsLeft)
        {
            Ship_straight = transform;
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + i);
        }
        if (IsCenter && BeforeLeft)
        {
            Ship_straight = transform;
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - i);
        }
        if (IsCenter && BeforeRight)
        {
            Ship_straight = transform;
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + i);
        }
    }
}