﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public int MaxPlayers = 2;
    public CarUI[] UI = new CarUI[2];
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
            Data[i].Rank = i +1;
        }
    }

    public void Update()
    {
        if (Data.Length <= 1) return;   
        //if both players are on the same lap
        if(Data[0].Lap == Data[1].Lap)
        {
            //if both players have the same last passed waypoint
            if(Data[0].Waypoint == Data[1].Waypoint)
            {
                //check distance between the last passed checkpoint and the player
                float distPlayer1 = Vector3.Distance(GetCheckPoint(0).position, GameObject.FindGameObjectWithTag("Player").transform.position);
                float distPlayer2 = Vector3.Distance(GetCheckPoint(1).position, GameObject.FindGameObjectWithTag("Player2").transform.position);

                //get current player data and store it in a variable
                VehicleData dataP1 = GetPlayer(0);
                VehicleData dataP2 = GetPlayer(1);

                //if dist between the last passed waypoint and player 2 is bigger than the last passed waypoint and player1. Set player 2 as first ranked
                if (distPlayer2 > distPlayer1)
                {
                    foreach (VehicleData data in Data)
                    {
                        if (dataP1.Rank == 2) continue;
                        if (data == dataP1) continue;

                            dataP1.Rank = data.Rank;
                            data.Rank--;
                        Debug.Log("player 1 ftw");
                    }
                    
                }
                //if dist between the last passed waypoint and player 1 is bigger than the last passed waypoint and player2. Set player 1 as first ranked
                else if (distPlayer1 > distPlayer2)
                {
                    foreach (VehicleData data in Data)
                    {
                        if (dataP2.Rank == 2) continue;
                        if (data == dataP2) continue;

                        dataP2.Rank = data.Rank;
                        data.Rank--;
                        Debug.Log("player 2 ftw");
                    }
                    
                }
                //update the Ui so the player can see which player is first
                for (int i = 0; i < MaxPlayers; i++)
                {
                    UI[i].SetData(Data[i]);
                }
            }
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
        UpdateRank(data);       

    }

    private void UpdateRank(VehicleData newData)
    {        
        foreach (VehicleData data in Data)
        {
            if (newData.Rank == 1) continue;
            if (data == newData || data.Rank > newData.Rank) continue;

            if (newData.Lap > data.Lap)
            {
                newData.Rank = data.Rank;
                data.Rank++;
            }
            else if (newData.Lap == data.Lap)
            {
                if (newData.Waypoint > data.Waypoint)
                {
                    newData.Rank = data.Rank;
                    data.Rank++;
                }
            }
        }       

        for (int i =0; i < MaxPlayers; i++)
        {
            UI[i].SetData(Data[i]);
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

            if (data.Lap == maxLap)
            {
                data.time = Time.timeSinceLevelLoad;
                data.Finished = true;

                foreach(VehicleData newdata in Data)
                {
                    if (!newdata.Finished) return;
                }
                FinishedRace();
            }
        }
    }


    private void FinishedRace()
    {
        //can make something happens if the race is finished here


        StartOptions.Instance.EndGameScreen(Data);
    }
    //gets the the latest checkpoint of the inserted playernumber
    public Transform GetCheckPoint(int playerNumber)
    {

        return Waypoints[Data[playerNumber].Waypoint];
    }
}