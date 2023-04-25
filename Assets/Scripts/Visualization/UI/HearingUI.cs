using Logic.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class HearingUI : MonoBehaviour
    {
        public IPlayerManager _playerManager;

        public float currentHearingCooldown;
        public float maxHearingCooldown;
        public GameObject xRayCamera;

        public Sprite[] hearingSprite;
        public Image hearingIcon;
        public Image filler;

        // Start is called before the first frame update
        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            currentHearingCooldown = 0;
        }

        // Update is called once per frame
        void Update()
        {
            // Activating X-Ray camera with "Q"
            if (currentHearingCooldown > 0)
            {
                currentHearingCooldown -= Time.deltaTime;
            }
            else
            {
                currentHearingCooldown = 0;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (_playerManager.GetHearing() >= 3f)
                    {
                        if (xRayCamera.activeInHierarchy)
                        {
                            xRayCamera.SetActive(false);
                        }
                        else
                        {
                            xRayCamera.SetActive(true);
                        }
                        currentHearingCooldown = maxHearingCooldown;
                    }
                }
            }

            filler.fillAmount = currentHearingCooldown / maxHearingCooldown;
        }

        private void OnEnable()
        {
            _playerManager.HearingChanged += UpdateHearing;
            UpdateHearing(_playerManager.GetHearing());
        }

        private void OnDisable()
        {
            _playerManager.HearingChanged -= UpdateHearing;
        }

        void UpdateHearing(float newValue)
        {
            if (newValue == 0)
            {
                AudioListener.volume = 0f;
                hearingIcon.sprite = hearingSprite[0];
            }
            else
            {
                AudioListener.volume = 1.0f;
                hearingIcon.sprite = hearingSprite[1];
            }
        }
    }
}
