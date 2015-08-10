using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float speed;

    public Vector3 dir;

    public GameObject ps;

    private bool isDead;

    public GameObject resetBtn;

    // Use this for initialization
    void Start()
    {
        isDead = false;
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            if (dir == Vector3.forward)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.forward;
            }
        }

        float amountToMove = speed * Time.deltaTime;

        transform.Translate(dir * amountToMove);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            other.gameObject.SetActive(false);
            Instantiate(ps, transform.position, Quaternion.identity);
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
