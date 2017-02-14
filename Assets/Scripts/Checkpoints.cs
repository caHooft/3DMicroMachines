using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public int MaxPlayers = 2;
    public VehicleData[] Data = new VehicleData[2];
    public Transform[] Waypoints = new Transform[4];
    public int maxLap;
    private static Checkpoints CheckPoints;

    //at the start of the game get the amount of players and make a easily accesable data vault for the amount of players
    private void Awake()
    {
        Data = new VehicleData[MaxPlayers];
        for(int i = 0; i < Data.Length; i++)
        {
            Data[i] = new VehicleData();
            Data[i].PlayerNumber = i;
        }
    }

    //checks if the static instance is set. If not it sets it
    public static Checkpoints Instance
    {
        get
        {
            if (!CheckPoints) CheckPoints = FindObjectOfType<Checkpoints>();
            return CheckPoints;
        }
    }
    //this function requires a playernumber
    private VehicleData GetPlayer(int playernumber)
    {

        VehicleData newdata = new VehicleData();

        //voor elke data 
        foreach (VehicleData data in Data)
        {
            if (data.PlayerNumber == playernumber)
            {
                return data;
            }

        }
        return newdata;
    }

    //this function requires a waypointNumber and a playernumber
    public void CheckPoint(int waypointNumber, int playernumber)
    {
        // then sets data to playernumber 
        VehicleData data = GetPlayer(playernumber);

        //it checks if the data isnt null before continueing
        if (data != null)
        {

            //checks if old waypoint is lesser than the new one
            if (data.Waypoint < waypointNumber)
            {
                //if thats true it replaces the waypoint with the new one
                data.Waypoint = waypointNumber;
            }
        }
      
            



    }
    //this function requires a playernumber
    public void Finish(int playernumber)
    {
        //gets the playernumber from data
        VehicleData data = GetPlayer(playernumber);
        //checks if the data is not null. If it's null the code does not continue
        if (data != null)
        {
            //resets waypoint to 0 so the new lap start with checkpoint 0
            data.Waypoint = 0;
            data.Lap++;

            if (data.Lap > maxLap)
            {
                FinishedRace();
            }
        }
    }

    private void FinishedRace()
    {   
        //can make what happens if the race is finished here
    }
    //gets the the latest checkpoint of the inserted playernumber
    public Transform GetCheckPoint(int playerNumber)
    {

        return Waypoints[Data[playerNumber].Waypoint];
    }

}
    

