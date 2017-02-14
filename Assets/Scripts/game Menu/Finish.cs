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

        //als de collider de player tag herkent voert hij de onderliggende code uit
        if (col.tag == "Player")
        {

            GameObject.Find("PanelPlayer1").GetComponentInChildren<CarUI>().NextLap();
                                                                                                                                                   

        }

        //als de collider de player2 tag herkent voert hij de onderliggende code uit
        if (col.tag == "Player2")
        {
            GameObject.Find("PanelPlayer2").GetComponentInChildren<CarUI>().NextLap();
          

        }

    }
}
/*
GameObject.Find("checkPoints").GetComponent<CheckPointManager>().GetLatest();
            if (lap == 3)
            {
                GameObject.Find("Panel").GetComponentInChildren<TimerScript>().Finnish();
            }

*/

/* if (col.tag == "Player2")
{
    //TimerScript.main.NextLap(col.tag);
    //GameObject.Find("Panel").GetComponentInChildren<TimerScript>().NextLap(col.tag);
    //TimerScript.main.NextLap(col.tag);
    //GameObject.Find("Panel").GetComponentInChildren<TimerScript>().NextLap(col.tag);

} */
