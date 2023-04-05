using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private StatePatternEnemy enemy;

    private int nextWaypoint; 

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Patrol();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player triggers the enemy hearing area -> Move to the Alert state");
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        // Ei voida k�ytt��, koska ollaan jo Patrol-tilassa. J�tet��n tyhj�ksi. 
    }

    public void ToTrackingState()
    {
        enemy.currentState = enemy.trackingState; 

    }

    void Look()
    {
        //Visualisoidaan s�de Scene ikkunassa.
        Debug.DrawRay(enemy.raycastSource.position, enemy.raycastSource.forward * enemy.sightRange, Color.green);
        RaycastHit hit; // Informaatio siit� mihin n�k�s�de osuu tallennetaan hit muuttujaan. 
        if(Physics.Raycast(enemy.raycastSource.position, enemy.raycastSource.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            // T�m� if toteutuu vain jos s�de osuu pelaajaan
            // Jos s�de osuu pelaajaan, enemy menee Chase-tilaan ja silloin my�s tiet��, ett� ChaseTarget on kappale johon s�de osui.
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }

    }

    void Patrol()
    {
        enemy.navMeshAgent.destination = enemy.waypoints[nextWaypoint].position;
        enemy.navMeshAgent.isStopped = false; 

        // Vaihdetaan Waypointia kun p��st��n nykyiseen waypointiin. Navmeshiss� on t�h�n ty�kalut
        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWaypoint = (nextWaypoint + 1) % enemy.waypoints.Length; // looppaa waypointit l�pi.        

        }

    }


}
