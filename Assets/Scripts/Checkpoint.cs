using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int WaypointNumber;

    //When someone triggers the checkpoint call pulltrigger from this parent and pass the collider
    private void OnTriggerEnter(Collider col)
    {
        CarMovement car = col.GetComponent<CarMovement>();
        if(car)
        {
            Checkpoints.Instance.CheckPoint(WaypointNumber, car.playernumber);
        }
            
    }
}
