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
        // Ei void käyttää, koska ollaan jo Alert tilassa
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
        //Visualisoidaan säde Scene ikkunassa.
        Debug.DrawRay(enemy.raycastSource.position, enemy.raycastSource.forward * enemy.sightRange, Color.yellow);
        RaycastHit hit; // Informaatio siitä mihin näkösäde osuu tallennetaan hit muuttujaan. 
        if (Physics.Raycast(enemy.raycastSource.position, enemy.raycastSource.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            // Tämä if toteutuu vain jos säde osuu pelaajaan
            // Jos säde osuu pelaajaan, enemy menee Chase-tilaan ja silloin myös tietää, että ChaseTarget on kappale johon säde osui.
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }

    }

    void Search()
    {
        enemy.navMeshAgent.isStopped = true; // Pysäytetään enemy alert tilassa
        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if(searchTimer >= enemy.searchDuration)
        {
            // Enemy väsyy etsimiseen, joten se palaa patrol tilaan. 
            ToPatrolState();

        }

    }


}
