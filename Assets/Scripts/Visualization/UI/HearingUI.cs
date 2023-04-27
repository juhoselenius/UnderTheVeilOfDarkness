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
        public float maxPulseCooldown;
        public float maxRayCooldown;
        public GameObject xRayCamera;

        public Sprite[] hearingSprite;
        public Image hearingIcon;
        public Image filler;

        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            currentHearingCooldown = 0;
        }

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

                        if(_playerManager.GetHearing() == 3f)
                        {
                            maxHearingCooldown = maxPulseCooldown;
                        }
                        else
                        {
                            maxHearingCooldown = maxRayCooldown;
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
            switch(newValue)
            {
                case 0:
                    AudioListener.volume = 0f;
                    hearingIcon.sprite = hearingSprite[0];
                    break;
                case 1:
                    AudioListener.volume = 1.0f;
                    hearingIcon.sprite = hearingSprite[1];
                    break;
                case 2:
                    AudioListener.volume = 1.0f;
                    hearingIcon.sprite = hearingSprite[2];
                    break;
                case 3:
                    AudioListener.volume = 1.0f;
                    hearingIcon.sprite = hearingSprite[3];
                    break;
                case 4:
                    AudioListener.volume = 1.0f;
                    hearingIcon.sprite = hearingSprite[4];
                    break;
            }
        }
    }
}
