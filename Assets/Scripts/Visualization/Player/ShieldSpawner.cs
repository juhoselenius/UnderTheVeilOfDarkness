using Logic.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization
{
    public class ShieldSpawner : MonoBehaviour
    {
        public IPlayerManager _playerManager;

        public GameObject[] shieldPrefabs;

        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(_playerManager.GetDefense() != 0)
                {
                    if(_playerManager.GetDefense() == 1)
                    {
                        GameObject shield = Instantiate(shieldPrefabs[(int)_playerManager.GetDefense()], gameObject.transform.position, gameObject.transform.rotation);
                    }
                    else
                    {
                        GameObject shield = Instantiate(shieldPrefabs[(int)_playerManager.GetDefense()], gameObject.transform, false);
                        shield.transform.parent = gameObject.transform;
                        shield.transform.localPosition = Vector3.zero;
                    }
                }
            }
        }
    }

}
