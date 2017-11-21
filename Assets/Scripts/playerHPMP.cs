using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHPMP : MonoBehaviour {

    public int maxHealth = 100;
    public int maxInk = 100;
    public int currentHealth = 80;
    public int currentInk = 60;
    public Text HP_display;
    public Text Ink_display;
    static KeyCode healButton = KeyCode.Z;
    static KeyCode teleportButton = KeyCode.X;
    static KeyCode rechargeButton = KeyCode.C;

    public GameObject teleportTester;

    // Use this for initialization
    void Start () {
        //HP_display = GetComponent<Text>();
        //Ink_display = GetComponent<Text>();
    }

    void healSpell()
    {
        //cost: 10 Ink
        //effect: heals 20 HP

        if (currentInk >= 10 && currentHealth < 100)
        {
            currentInk -= 10;
            currentHealth += 20;
        }

    }

    void teleportSpell()
    {
        //cost: 10 Ink
        //effect: teleports user forward a short distance.
        //direction of teleport depends on arrow direction held
        //if no direction is held, spell will not be cast.
        if (currentInk >= 10 && Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 current_pos = transform.position;
            current_pos.x += 5;
            teleportTester.transform.position = current_pos; //moves the tele tester to the potential teleport spot
            if (teleportTester.GetComponent<teleTester>().isInsideWall) //checks whether the tester collides with a wall
            {
                print("cannot teleport to right, as there is a wall");
                return; //abort, as we cannot teleport into a wall
            }
            else
            {
                transform.Translate(5, 0, 0);
                currentInk -= 10;
            }
            
        }
        else if (currentInk >= 10 && Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 current_pos = transform.position;
            current_pos.x -= 5;
            teleportTester.transform.position = current_pos; //moves the tele tester to the potential teleport spot
            if (teleportTester.GetComponent<teleTester>().isInsideWall) //checks whether the tester collides with a wall
            {
                print("cannot teleport to left, as there is a wall");
                return; //abort, as we cannot teleport into a wall
            }
            else
            {
                transform.Translate(-5, 0, 0);
                currentInk -= 10;
            }
        }
        
    }

    void rechargeSpell()
    {
        //fully recharges ink. used for testing purposes
        currentHealth = 80;
        currentInk = 100;
    }

    public void takeDamage(int dmg)
    {
        currentHealth -= dmg;
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(healButton))
        {
            healSpell();
        }
        else if (Input.GetKeyDown(teleportButton))
        {
            teleportSpell();
        }
        else if (Input.GetKeyDown(rechargeButton))
        {
            rechargeSpell();
        }

        HP_display.text = "HP: " + currentHealth + "/100";
        Ink_display.text = "Ink: " + currentInk + "/100";
    }
}
