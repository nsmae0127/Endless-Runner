using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // The player's movement speed
    public float speed;

    // The direction the player is traveling ing
    public Vector3 dir;

    // A reference to the player's particle system
    public GameObject ps;

    // Indicates if the player is dead
    private bool isDead;

    // A reference to the reset button
    public GameObject resetBtn;

    private int score = 0;

    public Text scoreText;

    // Use this for initialization
    void Start()
    {
        // Makes sure that the player is alive when the game starts
        isDead = false;

        // Sets the player's direction to 0, so that the player doesn't move when the game start
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // If we click on the screen or the first mouse button
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            score++;
            scoreText.text = score.ToString();

            // Switches the players direction every time we click on the screen or mouse
            if (dir == Vector3.forward)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.forward;
            }
        }

        // Calculates the player's movement
        float amountToMove = speed * Time.deltaTime;

        // Makes the player move
        transform.Translate(dir * amountToMove);
    }

    // When the player enters a trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup") // If its a pick up able objects
        {
            other.gameObject.SetActive(false);
            Instantiate(ps, transform.position, Quaternion.identity);
            score += 3;
            scoreText.text = score.ToString();
        }
    }

    // When the player exits a trigger
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tile")
        {
            RaycastHit hit;

            Ray downRay = new Ray(transform.position, -Vector3.up);

            if (!Physics.Raycast(downRay, out hit))
            {
                // Kill player!
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
