using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Chase();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        // Ei k‰ytet‰, koska ollaan jo chase tilassa
    }

    public void ToPatrolState()
    {
        // T‰t‰ harvemmin k‰ytet‰‰n. J‰tet‰‰n toistaiseksi tyhj‰ksi. 
    }

    public void ToTrackingState()
    {
        enemy.currentState = enemy.trackingState;

    }

    void Look()
    {

        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.raycastSource.position; // Vektori silm‰st‰ pelaajaan. 


        //Visualisoidaan s‰de Scene ikkunassa.
        Debug.DrawRay(enemy.raycastSource.position, enemyToTarget, Color.red);
        RaycastHit hit; // Informaatio siit‰ mihin n‰kˆs‰de osuu tallennetaan hit muuttujaan. 
        if (Physics.Raycast(enemy.raycastSource.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            // Enemyn silm‰t on pelaajassa kiinni.
            enemy.chaseTarget = hit.transform;

        }
        else
        {
            // Enemy jahtaa pelaajaa, mutta ei en‰‰ n‰e sit‰.  => Menn‰‰n Tracking tilaan. 
            enemy.lastKnownPlayerPosition = enemy.chaseTarget.position;
            ToTrackingState();

        }

    }

    void Chase()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false; 

    }


}
