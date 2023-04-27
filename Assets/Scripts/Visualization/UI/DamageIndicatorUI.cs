using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code from the tutorial "Unity Tutorial - How to create a Damage Indicator"
 * by WatchFindDo Media on YouTube (https://youtu.be/BC3AKOQUx04)
*/

namespace Visualization
{
    public class DamageIndicatorUI : MonoBehaviour
    {
        [SerializeField] private DamageIndicator indicatorPrefab;
        [SerializeField] private RectTransform holder;
        [SerializeField] private new Camera camera;
        [SerializeField] private Transform player;

        private Dictionary<Transform, DamageIndicator> indicators = new Dictionary<Transform, DamageIndicator>();

        public static Action<Transform> CreateIndicator = delegate { };
        public static Func<Transform, bool> CheckIfObjectInSight = null;

        private void OnEnable()
        {
            CreateIndicator += Create;
            CheckIfObjectInSight += InSight;
        }

        private void OnDisable()
        {
            CreateIndicator -= Create;
            CheckIfObjectInSight -= InSight;
        }

        void Create(Transform target)
        {
            if(indicators.ContainsKey(target))
            {
                indicators[target].Restart();
                return;
            }

            DamageIndicator newIndicator = Instantiate(indicatorPrefab, holder);
            newIndicator.Register(target, player, new Action( () => { indicators.Remove(target); }));

            indicators.Add(target, newIndicator);
        }

        bool InSight(Transform t)
        {
            Vector3 screenpoint = camera.WorldToViewportPoint(t.position);
            return screenpoint.z > 0 && screenpoint.x > 0 && screenpoint.x < 1 && screenpoint.y > 0 && screenpoint.y < 1;
        }
    }

}
