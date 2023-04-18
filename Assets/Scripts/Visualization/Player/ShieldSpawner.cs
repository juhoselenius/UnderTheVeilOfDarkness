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

        // Start is called before the first frame update
        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(_playerManager.GetDefense() != 0)
                {
                    if(_playerManager.GetDefense() == 1)
                    {
                        Instantiate(shieldPrefabs[(int)_playerManager.GetDefense()], gameObject.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        GameObject shield = Instantiate(shieldPrefabs[(int)_playerManager.GetDefense()], gameObject.transform.position, Quaternion.identity);
                        shield.transform.parent = gameObject.transform;
                    }
                }
            }
        }
    }

}
