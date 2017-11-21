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

    bool grounded = false;

    //direction facing
    bool facingright = true;

    float MoveVelocity;
    // Use this for initialization
    void Start()
    {
        //current_health = max_health;

    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();



    }

    //Check to see if grounded or not
    void OnTriggerEnter2D()
    {
        grounded = true;
    }
    void OnTriggerExit2D()
    {
        grounded = false;
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



        //move
        MoveVelocity = 0;
       
       
            if (Input.GetKey(KeyCode.A))
            {
                MoveVelocity = -Max_Speed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                MoveVelocity = Max_Speed;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(MoveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        





        //attack
        if (Input.GetKeyDown(KeyCode.Z))
        {

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



