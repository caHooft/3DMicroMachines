using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        //calls finish funtion in chechpoints to reset checkpoints to 0 for respawing and position
        CarMovement car = col.GetComponent<CarMovement>();
        if (!car) return;
        {
            Checkpoints.Instance.Finish(car.playernumber);
        }

    }
}
