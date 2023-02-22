using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float speed; //to give the player a speed


    private Vector3 dir;//to find which direction player moves in and to change the direction

    private float amountToMove; //the amount to move the object everytime the update is called

    public GameObject ps;

    private bool isDead;

    public GameObject resetBtn;

    private int score = 0;

    public Text scoreText;

    void Start()
    {
        isDead = false;
        dir = Vector3.zero; //to keep the ball from Not moving in the starting of the game.
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead) //upon user input
        {
            score++;

            scoreText.text = score.ToString();

            if (dir == Vector3.forward)
            {
                dir = Vector3.left;
            }

            else
            {
                dir = Vector3.forward;
            }
        }

        amountToMove = speed * Time.deltaTime; //to calculate THAT Amount to move float variable taken above in the update function. Update is called different time upon the FPS, So delta time allows us to run the object at the same time in every device.


        transform.Translate(dir * amountToMove); // to change the position of player with transform component
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            other.gameObject.SetActive(false);
            Instantiate(ps, transform.position, Quaternion.identity);
            score+= 3;

            scoreText.text = score.ToString();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tile")
        {
            RaycastHit hit;

            Ray downRay = new Ray(transform.position, -Vector3.up);

            if (!Physics.Raycast(downRay, out hit))
            {
                //kill player

                isDead = true;

                resetBtn.SetActive(true);

                if (transform.childCount > 0)
                {
                  transform.GetChild(0).transform.parent = null;
                }

            }
        }
    }
}
