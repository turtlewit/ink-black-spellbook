using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleTester : MonoBehaviour {

    public bool isInsideWall = false;
    private void OnColliderEnter2D(Collider2D collision)
    {
        print("object entered collision");
        if (collision.gameObject.CompareTag("wall"))
        {
            isInsideWall = true;
        }
    }

    private void OnColliderStay2D(Collider2D collision)
    {
        print("object stayed in collision");
        if (collision.gameObject.CompareTag("wall"))
        {
            isInsideWall = true;
        }
    }

    private void OnColliderExit2D(Collider2D collision)
    {
        print("object exited collision");
        if (collision.gameObject.CompareTag("wall"))
        {
            isInsideWall = false;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
