﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSoccer : Agent
{

	public enum Team
    {
        red, blue
    }
	public enum AgentRole
    {
        striker, defender, goalie
    }
	// ReadRewardData readRewardData;
	public Team team;
	public AgentRole agentRole;
	// public float teamFloat;
	// public float playerID;
	public int playerIndex;
	public SoccerFieldArea area;
	[HideInInspector]
	public Rigidbody agentRB;
	[HideInInspector]
	// public Vector3 startingPos;

	public List<float> myState = new List<float>(); //list for state data. to be updated every FixedUpdate in this script
	SoccerAcademy academy;
	Renderer renderer;


	public void ChooseRandomTeam()
	{
		team = (Team)Random.Range(0,2);
		renderer.material = team == Team.red? academy.redMaterial: academy.blueMaterial;
	}

	public void JoinRedTeam(AgentRole role)
	{
		agentRole = role;
		team = Team.red;
		// area.playerStates[playerIndex].a
		renderer.material = academy.redMaterial;
	}

	public void JoinBlueTeam(AgentRole role)
	{
		agentRole = role;
		team = Team.blue;
		// area.playerStates[playerIndex].a
		renderer.material = academy.blueMaterial;
	}

    void Awake()
    {
		renderer = GetComponent<Renderer>();
		academy = FindObjectOfType<SoccerAcademy>(); //get the academy
		// readRewardData = FindObjectOfType<ReadRewardData>(); //get reward data script


		// brain = agentRole == AgentRole.striker? academy.redBrainStriker: agentRole == AgentRole.defender? academy.redBrainDefender: academy.redBrainGoalie;
		brain = agentRole == AgentRole.striker? academy.brainStriker: agentRole == AgentRole.defender? academy.redBrainDefender: academy.brainGoalie;
		PlayerState playerState = new PlayerState();
		// playerState.teamFloat = 0;
		// teamFloat = 0;
		// playerState.agentRoleFloat = agentRole == AgentRole.striker? 0: agentRole == AgentRole.defender? 1: 2;
		// playerState.currentTeamFloat = (float)Random.Range(0,2); //return either a 0 or 1 * max is exclusive ex: Random.Range(0,10) will pick a int between 0-9
		// playerState.agentScript.team = (Team)Random.Range(0,2);

		// ChooseRandomTeam();
		// SetPlayerColor();
		maxStep = academy.maxAgentSteps;
		// playerState.playerID = area.redPlayers.Count; //float id used to id individual
		// playerID = playerState.playerID;
		playerState.agentRB = GetComponent<Rigidbody>(); //cache the RB
		agentRB = GetComponent<Rigidbody>(); //cache the RB
		playerState.startingPos = transform.position;
		playerState.agentScript = this;
		// playerState.targetGoal = area.blueGoal;
		// playerState.defendGoal = area.redGoal;
		// area.redPlayers.Add(playerState);
		area.playerStates.Add(playerState);
		playerIndex = area.playerStates.IndexOf(playerState);
		playerState.playerIndex = playerIndex;

		// //we need to set up each player. most of this is unused right now but will be useful when we start collecting other player's states
        // if(team == Team.red)
        // {
		// 	// brain = agentRole == AgentRole.striker? academy.redBrainStriker: agentRole == AgentRole.defender? academy.redBrainDefender: academy.redBrainGoalie;
		// 	// brain = agentRole == AgentRole.striker? academy.brainStriker: agentRole == AgentRole.defender? academy.redBrainDefender: academy.brainGoalie;
		// 	// PlayerState playerState = new PlayerState();
		// 	playerState.currentTeamFloat = 0;
		// 	// teamFloat = 0;
		// 	// playerState.agentRoleFloat = agentRole == AgentRole.striker? 0: agentRole == AgentRole.defender? 1: 2;
		// 	// maxStep = academy.maxAgentSteps;
		// 	// playerState.playerID = area.redPlayers.Count; //float id used to id individual
		// 	// playerID = playerState.playerID;
		// 	// playerState.agentRB = GetComponent<Rigidbody>(); //cache the RB
		// 	// agentRB = GetComponent<Rigidbody>(); //cache the RB
		// 	// playerState.startingPos = transform.position;
		// 	// playerState.agentScript = this;
		// 	// playerState.targetGoal = area.blueGoal;
		// 	// playerState.defendGoal = area.redGoal;
        //     // area.redPlayers.Add(playerState);
        //     area.playerStates.Add(playerState);
		// 	playerIndex = area.playerStates.IndexOf(playerState);
		// 	playerState.playerIndex = playerIndex;

        // }
        // else if(team == Team.blue)
        // {
		// 	// brain = agentRole == AgentRole.striker? academy.blueBrainStriker: agentRole == AgentRole.defender? academy.blueBrainDefender: academy.blueBrainGoalie;
		// 	brain = agentRole == AgentRole.striker? academy.brainStriker: agentRole == AgentRole.defender? academy.blueBrainDefender: academy.brainGoalie;
		// 	PlayerState playerState = new PlayerState();
		// 	playerState.currentTeamFloat = 1;
		// 	teamFloat = 1;
		// 	playerState.agentRoleFloat = agentRole == AgentRole.striker? 0: agentRole == AgentRole.defender? 1: 2;
		// 	maxStep = academy.maxAgentSteps;
		// 	playerState.playerID = area.bluePlayers.Count; //float id used to id individual
		// 	playerID = playerState.playerID;
		// 	playerState.agentRB = GetComponent<Rigidbody>(); //cache the RB
		// 	agentRB = GetComponent<Rigidbody>(); //cache the RB
		// 	playerState.startingPos = transform.position;
		// 	playerState.agentScript = this;
		// 	// playerState.targetGoal = area.redGoal;
		// 	// playerState.defendGoal = area.blueGoal;
        //     // area.bluePlayers.Add(playerState);
        //     area.playerStates.Add(playerState);
		// 	playerIndex = area.playerStates.IndexOf(playerState);
		// 	playerState.playerIndex = playerIndex;
        // }


		// //we need to set up each player. most of this is unused right now but will be useful when we start collecting other player's states
        // if(team == Team.red)
        // {
		// 	// brain = agentRole == AgentRole.striker? academy.redBrainStriker: agentRole == AgentRole.defender? academy.redBrainDefender: academy.redBrainGoalie;
		// 	brain = agentRole == AgentRole.striker? academy.brainStriker: agentRole == AgentRole.defender? academy.redBrainDefender: academy.brainGoalie;
		// 	PlayerState playerState = new PlayerState();
		// 	playerState.teamFloat = 0;
		// 	teamFloat = 0;
		// 	playerState.agentRoleFloat = agentRole == AgentRole.striker? 0: agentRole == AgentRole.defender? 1: 2;
		// 	maxStep = academy.maxAgentSteps;
		// 	playerState.playerID = area.redPlayers.Count; //float id used to id individual
		// 	playerID = playerState.playerID;
		// 	playerState.agentRB = GetComponent<Rigidbody>(); //cache the RB
		// 	agentRB = GetComponent<Rigidbody>(); //cache the RB
		// 	playerState.startingPos = transform.position;
		// 	playerState.agentScript = this;
		// 	playerState.targetGoal = area.blueGoal;
		// 	playerState.defendGoal = area.redGoal;
        //     area.redPlayers.Add(playerState);
        //     area.playerStates.Add(playerState);
		// 	playerIndex = area.playerStates.IndexOf(playerState);
		// 	playerState.playerIndex = playerIndex;

        // }
        // else if(team == Team.blue)
        // {
		// 	// brain = agentRole == AgentRole.striker? academy.blueBrainStriker: agentRole == AgentRole.defender? academy.blueBrainDefender: academy.blueBrainGoalie;
		// 	brain = agentRole == AgentRole.striker? academy.brainStriker: agentRole == AgentRole.defender? academy.blueBrainDefender: academy.brainGoalie;
		// 	PlayerState playerState = new PlayerState();
		// 	playerState.teamFloat = 1;
		// 	teamFloat = 1;
		// 	playerState.agentRoleFloat = agentRole == AgentRole.striker? 0: agentRole == AgentRole.defender? 1: 2;
		// 	maxStep = academy.maxAgentSteps;
		// 	playerState.playerID = area.bluePlayers.Count; //float id used to id individual
		// 	playerID = playerState.playerID;
		// 	playerState.agentRB = GetComponent<Rigidbody>(); //cache the RB
		// 	agentRB = GetComponent<Rigidbody>(); //cache the RB
		// 	playerState.startingPos = transform.position;
		// 	playerState.agentScript = this;
		// 	playerState.targetGoal = area.redGoal;
		// 	playerState.defendGoal = area.blueGoal;
        //     area.bluePlayers.Add(playerState);
        //     area.playerStates.Add(playerState);
		// 	playerIndex = area.playerStates.IndexOf(playerState);
		// 	playerState.playerIndex = playerIndex;
        // }

        // startingPos = transform.position; //cache the starting pos in case we want to spawn players back at their startingpos
    }

    public override void InitializeAgent()
    {
		base.InitializeAgent();
    }

  	public override List<float> CollectState()
    {
		myState = area.playerStates[playerIndex].state; //states for all players are collected in the SoccerFieldArea script. we can pull this player's state by index. This will be useful when we are tracking other players
		state.AddRange(myState);
		return state;
	}

	public void MoveAgent(float[] act) {

        if (brain.brainParameters.actionSpaceType == StateType.continuous)
        {

			if(act[0] != 0)
			{
				float energyConservationPentalty = Mathf.Abs(act[0])/1000;
				// print("act[0] = " + act[0]);
				reward -= energyConservationPentalty;
				// reward -= .0001f;
			}
			if(act[1] != 0)
			{
				float energyConservationPentalty = Mathf.Abs(act[1])/1000;
				// print("act[1] = " + act[1]);
				reward -= energyConservationPentalty;
			}
			// if(act[2] != 0)
			// {
			// 	float energyConservationPentalty = Mathf.Abs(act[2])/1000;
			// 	// print("act[2] = " + act[2]);
			// 	reward -= energyConservationPentalty;
			// }
			// Vector3 directionX = Vector3.right * Mathf.Clamp(act[0], -1f, 1f);  //go left or right in world space
            // Vector3 directionZ = Vector3.forward * Mathf.Clamp(act[1], -1f, 1f); //go forward or back in world space
        	// Vector3 dirToGo = directionX + directionZ; //the dir we want to go
			// agentRB.AddForce(dirToGo * academy.agentRunSpeed, ForceMode.VelocityChange); //GO

			// agentRB.AddTorque(transform.up * Mathf.Clamp(act[2], -1f, 1f) * academy.agentRotationSpeed, ForceMode.VelocityChange); //turn right or left
			
			
			Vector3 directionX = Vector3.right * act[0];  //go left or right in world space
            Vector3 directionZ = Vector3.forward * act[1]; //go forward or back in world space
        	Vector3 dirToGo = directionX + directionZ; //the dir we want to go
			agentRB.AddForce(dirToGo * academy.agentRunSpeed, ForceMode.VelocityChange); //GO
			if(dirToGo != Vector3.zero)
			{
				agentRB.rotation = Quaternion.Lerp(agentRB.rotation, Quaternion.LookRotation(dirToGo), Time.deltaTime * academy.agentRotationSpeed);
				// agentRB.rotation = Quaternion.LookRotation(dirToGo);
			}

			// agentRB.transform.LookAt(dirToGo);

			// agentRB.AddTorque(transform.up * act[2] * ascademy.agentRotationSpeed, ForceMode.VelocityChange); //turn right or left

        }
    }

	public override void AgentStep(float[] act)
	{
		// print(readRewardData.currentMeanReward);
		// reward += .0001f; //mainly for goalies. not sure how this will affect offense. idea is to stay alive longer
		// reward -= .0005f; //hurry up
        MoveAgent(act); //perform agent actions
		// print(brain.name);
		// if(readRewardData.rewardDataDict.ContainsKey(brain.name) && readRewardData.rewardDataDict[brain.name].currentMeanReward > -.8)
		// {

		// 	// print("reward > .8");
		// }
	}

	public override void AgentReset()
	{
		transform.position =  area.GetRandomSpawnPos();
		agentRB.velocity = Vector3.zero; //we want the agent's vel to return to zero on reset
	}


	public override void AgentOnDone()
	{

	}
}
