using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    private StatePatternEnemy enemy;
    private float searchTimer; 

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        // Ei void k�ytt��, koska ollaan jo Alert tilassa
    }

    public void ToChaseState()
    {
        searchTimer = 0; 
        enemy.currentState = enemy.chaseState; 
    }

    public void ToPatrolState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.patrolState; 
    }

    public void ToTrackingState()
    {
        enemy.currentState = enemy.trackingState; 

    }

    void Look()
    {
        //Visualisoidaan s�de Scene ikkunassa.
        Debug.DrawRay(enemy.raycastSource.position, enemy.raycastSource.forward * enemy.sightRange, Color.yellow);
        RaycastHit hit; // Informaatio siit� mihin n�k�s�de osuu tallennetaan hit muuttujaan. 
        if (Physics.Raycast(enemy.raycastSource.position, enemy.raycastSource.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            // T�m� if toteutuu vain jos s�de osuu pelaajaan
            // Jos s�de osuu pelaajaan, enemy menee Chase-tilaan ja silloin my�s tiet��, ett� ChaseTarget on kappale johon s�de osui.
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }

    }

    void Search()
    {
        enemy.navMeshAgent.isStopped = true; // Pys�ytet��n enemy alert tilassa
        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if(searchTimer >= enemy.searchDuration)
        {
            // Enemy v�syy etsimiseen, joten se palaa patrol tilaan. 
            ToPatrolState();

        }

    }


}
