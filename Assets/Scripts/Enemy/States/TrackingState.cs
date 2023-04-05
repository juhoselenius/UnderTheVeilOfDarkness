using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingState : IEnemyState
{
    private StatePatternEnemy enemy;

    public TrackingState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Hunt();
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
        // Ei voida käyttää, koska ollaan jo Patrol-tilassa. Jätetään tyhjäksi. 
    }

    public void ToTrackingState()
    {

    }

    void Look()
    {
        //Visualisoidaan säde Scene ikkunassa.
        Debug.DrawRay(enemy.raycastSource.position, enemy.raycastSource.forward * enemy.sightRange, Color.green);
        RaycastHit hit; // Informaatio siitä mihin näkösäde osuu tallennetaan hit muuttujaan. 
        if (Physics.Raycast(enemy.raycastSource.position, enemy.raycastSource.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            // Tämä if toteutuu vain jos säde osuu pelaajaan
            // Jos säde osuu pelaajaan, enemy menee Chase-tilaan ja silloin myös tietää, että ChaseTarget on kappale johon säde osui.
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    void Hunt()
    {
        enemy.navMeshAgent.destination = enemy.lastKnownPlayerPosition;
        enemy.navMeshAgent.isStopped = false;

        // Mennään alert-tilaan kun päästään viimeksi tiedettyyn pelaajan sijantiin. Navmeshissä on tähän työkalut
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            ToAlertState();       
        }
    }
}
