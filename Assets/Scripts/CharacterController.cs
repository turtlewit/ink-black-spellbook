using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    //stats
    //    public int current_health;
    //  public int max_health;



    //speed
    public float Max_Speed = 10f;

    //jump
    public float jump = 5f;

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
        Debug.Log("touching ground");
        grounded = true;
    }
    void OnTriggerExit2D()
    {
        Debug.Log("not touching ground");
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
        if (Input.GetKey(KeyCode.UpArrow))
        {

            if (grounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            }
        }





        //move
        MoveVelocity = 0;
       
       
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveVelocity = -Max_Speed;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
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



        //flipping
        if (MoveVelocity > 0 && !facingright)
            Flip();
        else if (MoveVelocity < 0 && facingright)
            Flip();

    }
}



