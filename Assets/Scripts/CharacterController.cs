using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    //stats
    //    public int current_health;
    //  public int max_health;



    //speed
    public float Max_Speed = 7f;

    //jump
    public float jump = 8f;
	public float High_Jump = 12f;
	public float midair_lerp_sensitivity;

    bool grounded = false;

    //direction facing
    bool facingright = true;

    float MoveVelocity;

	//animator
	public Animator pa;
	public GameObject broom;

	//crouching
	private bool crouching = false;
	public BoxCollider2D small;
	public BoxCollider2D large;

	//camera
	public Camera c;
	private Vector3 init_cam_pos;
	public float camera_lerp_sensitivity;

    // Use this for initialization
    void Start()
    {
        //current_health = max_health;
		init_cam_pos = c.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();

		if (!grounded) { 
			SetAnimator ("is_running", false);
			SetAnimator ("is_jumping", true);
		} else {
			SetAnimator ("is_jumping", false);
		}

		SetCollider ();

    }

	void SetAnimator(string name, bool value)
	{
		//Sets the boolean to the requested value
		//only if the value is different.

		if ((pa.GetBool (name) != value)) {
			pa.SetBool (name, value);
		}
	}

	void SetCollider(){
		if (crouching) {
			if (!small.enabled) {
				small.enabled = true;
				large.enabled = false;
			}
			c.transform.localPosition = Vector3.Lerp (
				c.transform.localPosition, 
				new Vector3 (c.transform.localPosition.x, -1.5f, c.transform.localPosition.z), 
				camera_lerp_sensitivity * Time.deltaTime
			);
		} else {
			if (!large.enabled) {
				large.enabled = true;
				small.enabled = false;
			}
			if (c.transform.position != init_cam_pos) {
				c.transform.localPosition = init_cam_pos;
			}
		}
	}

    //Check to see if grounded or not
	void OnTriggerStay2D(Collider2D col)
    {
		if (col.tag != "Contact Enemy") {
			Debug.Log ("touching ground");
			grounded = true;
		}
    }
	void OnTriggerExit2D(Collider2D col)
    {
		if (col.tag != "Contact Enemy") {
			Debug.Log ("not touching ground");
			grounded = false;
		}
    }


    //Flip character
    void Flip()
    {
        facingright = !facingright;

        Vector3 the_scale = transform.localScale;

        the_scale.x *= -1;

        transform.localScale = the_scale;
    }

    private void HandleInput()
    {
        //jumping

        if (Input.GetKeyDown(KeyCode.W))

        {

            if (grounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            }
        }

		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
		{
			if (grounded)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, High_Jump);
			}
		}


		crouching = false;

        //move

		MoveVelocity = 0;
		
		if (Input.GetKey (KeyCode.S)) {
			SetAnimator ("is_crouching", true);
			SetAnimator ("is_running", false);
			crouching = true;
		} else if (Input.GetKey (KeyCode.A)) {
			MoveVelocity = -Max_Speed;
			SetAnimator ("is_running", true); //Trigger running animation.
			SetAnimator ("is_crouching", false);
		} else if (Input.GetKey (KeyCode.D)) {
			MoveVelocity = Max_Speed;
			SetAnimator ("is_running", true); //Trigger running animation.
			SetAnimator ("is_crouching", false);
		} else {
			SetAnimator ("is_crouching", false);
			SetAnimator ("is_running", false);
		}
		if (grounded) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (MoveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
		} else {
			GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp (
				GetComponent<Rigidbody2D> ().velocity, 
				new Vector2 (MoveVelocity, GetComponent<Rigidbody2D> ().velocity.y), 
				midair_lerp_sensitivity * Time.deltaTime
			);
		}

        //attack
        if (Input.GetKeyDown(KeyCode.J))
        {
			pa.SetTrigger ("Attack");
            Collider2D[] hitobjects = Physics2D.OverlapCircleAll(transform.position, 1.0f);
            if (hitobjects.Length >= 5)
            {
                for (int i = 0; i <= hitobjects.Length - 1; i++)
                {
                    if (hitobjects[i].gameObject.tag == "enemy")
                    {
                        hitobjects[i].SendMessage("damage", 10.0f, SendMessageOptions.DontRequireReceiver);
                    }

                }
            }
        }


		if (Input.GetKeyDown(KeyCode.L))
		{
			//subtract mana
			Collider2D[] hitobjects = Physics2D.OverlapCircleAll(transform.position, 3.0f);
			if (hitobjects.Length >= 5)
			{
				for (int i = 0; i <= hitobjects.Length - 1; i++)
				{
					if (hitobjects[i].gameObject.tag == "enemy")
					{
						hitobjects[i].SendMessage("lightning", 15.0f, SendMessageOptions.DontRequireReceiver);
					}

				}
			}
		}



        //flipping
        if (MoveVelocity > 0 && !facingright)
            Flip();
        else if (MoveVelocity < 0 && facingright)
            Flip();

    }
}



