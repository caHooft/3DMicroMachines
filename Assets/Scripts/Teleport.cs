using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Vector3 latestPoint;
    private Quaternion latestRotation;
    public GameObject car;
    public GameObject car2;


    void OnTriggerEnter(Collider col)
    {
        //if the player collides with a gameObject with the tag player run this code
        if (col.tag == "Player")
        {
            //Creates a gameobject that gets the GameObject respawnPoint from the CarMovement script off of the collider.
             Transform respawnPoint = Checkpoints.Instance.GetCheckPoint(0);

            //Destroys the Parent Gameobject that triggers with the collider
            Destroy(col.transform.parent.gameObject);

            //Creates a new car at the position of the respawnpoint with the rotation of the respawnpoint.
            Instantiate(car, respawnPoint.transform.position, respawnPoint.transform.rotation);
        }
        if (col.tag == "Player2")
        {
            //Creates a gameobject that gets the GameObject respawnPoint from the CarMovement script off of the collider.
            
            Transform respawnPoint = Checkpoints.Instance.GetCheckPoint(1);
            
            //GameObject respawnPoint = col.GetComponent<CarMovement>().respawnPoint;

            //Destroys the Parent Gameobject that triggers with the collider
            Destroy(col.transform.parent.gameObject);

            //Creates a new car at the position of the respawnpoint with the rotation of the respawnpoint.
            
            Instantiate(car2, respawnPoint.transform.position, respawnPoint.transform.rotation);
        }
    }

}

